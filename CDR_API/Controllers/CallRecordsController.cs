using CDR_API.Models;
using CDR_API.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CDR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CallRecordsController : ControllerBase
    {
        private readonly IFileReadService fileReadService;
        private readonly IRecordsProcessingService recordsStoreService;

        public CallRecordsController(IFileReadService fileReadService, IRecordsProcessingService recordsStoreService)
        {
            this.fileReadService = fileReadService;
            this.recordsStoreService = recordsStoreService;
        }

        /// <summary>
        /// File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile([FromForm] UploadFileModel file)
        {
            if (file == null)
            {
                return BadRequest();
            }

            try
            {
                await fileReadService.ToReadFile(file);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the total number of calls within a given time frame.
        /// </summary>
        /// <param name="startDate">The start date of the period to retrieve call data for. Expected format: YYYY-MM-DD.</param>
        /// <param name="endDate">The end date of the period to retrieve call data for. Expected format: YYYY-MM-DD.</param>
        /// <returns>The total number of calls made between the start and end dates.</returns>
        [HttpGet("total-calls")]
        public async Task<ActionResult<int>> GetTotalCalls(DateTime startDate, DateTime endDate)
        {
            try
            {
                var amount = await recordsStoreService.GetTotalCalls(startDate, endDate);
                return Ok(amount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the average duration of calls within a given time frame in seconds.
        /// </summary>
        /// <param name="startDate">The start date of the period to retrieve call data for. Expected format: YYYY-MM-DD.</param>
        /// <param name="endDate">The end date of the period to retrieve call data for. Expected format: YYYY-MM-DD.</param>
        /// <returns>The total number of calls made between the start and end dates.</returns>
        [HttpGet("average-duration")]
        public async Task<ActionResult<double>> GetAverageCallDuration(DateTime startDate, DateTime endDate)
        {
            try
            {
                var averageDuration = await recordsStoreService.GetAverageCallDuration(startDate, endDate);
                return Ok(averageDuration);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lists the top 10 most frequently called numbers.
        /// </summary>
        /// <returns>A list of the top 10 most frequently called numbers.</returns>
        [HttpGet("top-called")]
        public async Task<ActionResult<IEnumerable<string>>> GetTopCalledNumbers()
        {
            try
            {
                var topCalledNumbers = await recordsStoreService.GetTopCalledNumbers();
                return Ok(topCalledNumbers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Provides an analysis of call volumes distributed by time of day for the specified date.
        /// </summary>
        /// <param name="date">The date for which to analyze call volume by hour.</param>
        /// <returns>A list of objects each representing an hour of the day and the volume of calls in that hour.</returns>
        [HttpGet("volume-by-time")]
        public ActionResult<IEnumerable<dynamic>> GetCallVolumeByTimeOfDay(DateTime date)
        {
            try
            {
                var volumes = recordsStoreService.GetCallVolumeByTimeOfDay(date);
                return Ok(volumes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Calculates the total cost of calls within a specified period.
        /// </summary>
        /// <param name="startDate">The start date of the period for which to calculate the total cost.</param>
        /// <param name="endDate">The end date of the period for which to calculate the total cost.</param>
        /// <returns>The total cost of calls made within the specified date range.</returns>
        [HttpGet("cost-analysis")]
        public async Task<ActionResult<decimal>> GetCostAnalysis(DateTime startDate, DateTime endDate)
        {
            try
            {
                var totalCost = await recordsStoreService.GetCostAnalysis(startDate, endDate);
                return Ok(totalCost);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
