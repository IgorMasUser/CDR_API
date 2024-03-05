using Moq;
using Microsoft.EntityFrameworkCore;
using CDR_API.Services.Abstraction;
using CDR_API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CDR_API.Data;
using CDR_API.Services.Impl;

namespace CDR_API_tests
{
    public class CdrTests
    {
        [Fact]
        public async Task GetCostAnalysis_ReturnsTotalCost()
        {
            // Arrange
            var mockService = new Mock<IRecordsProcessingService>();
            var controller = new CallRecordsController(null, mockService.Object);
            var startDate = new DateTime(2020, 1, 1);
            var endDate = new DateTime(2020, 1, 31);
            var expectedCost = 100.50m;

            mockService.Setup(s => s.GetCostAnalysis(startDate, endDate))
                       .ReturnsAsync(expectedCost);

            // Act
            var result = await controller.GetCostAnalysis(startDate, endDate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualCost = Assert.IsType<decimal>(okResult.Value);
            Assert.Equal(expectedCost, actualCost);
        }

        [Fact]
        public async Task GetUnusualActivity_WhenCalledWithThreshold_ReturnsThresholdCallerIds()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CdrContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var arrangeContext = new CdrContext(options))
            {
                arrangeContext.CallRecords.AddRange(new MockCallRecords().callRecords);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CdrContext(options))
            {
                var service = new RecordsProcessingService(actContext);
                var threshold = 3;

                // Act
                var result = await service.GetUnusualActivity(threshold);

                // Assert
                Assert.DoesNotContain("441215598896", result);
                Assert.Contains("443330132430", result);
            }
        }


        [Fact]
        public async Task GetTopCalledNumbers_ReturnsTopCalledNumbers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CdrContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var arrangeContext = new CdrContext(options))
            {
                arrangeContext.CallRecords.AddRange(new MockCallRecords().callRecords);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CdrContext(options))
            {
                var service = new RecordsProcessingService(actContext);
                var expectedTopOneCalledNumbers = "448000480968";

                // Act
                var result = await service.GetTopCalledNumbers();

                // Assert

                Assert.Contains(expectedTopOneCalledNumbers, result);
            }
        }
    }
}