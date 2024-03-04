using CDR_API.Data;
using CDR_API.Models;
using CDR_API.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CDR_API.Services.Impl
{
    public class RecordsProcessingService : IRecordsProcessingService
    {
        private readonly CdrContext cdrContext;

        public RecordsProcessingService(CdrContext cdrContext)
        {
            this.cdrContext = cdrContext ?? throw new ArgumentNullException(nameof(cdrContext));
        }

        public async Task<double> GetAverageCallDuration(DateTime startDate, DateTime endDate)
        {
            try
            {
                var averageDuration = await cdrContext.CallRecords
                         .Where(c => c.CallDate >= startDate && c.CallDate <= endDate)
                         .AverageAsync(c => c.Duration);

                return averageDuration;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting average call records duration from the database.", ex);
            }
        }

        public IEnumerable<dynamic> GetCallVolumeByTimeOfDay(DateTime date)
        {
            try
            {
                var volumes = cdrContext.CallRecords
                     .Where(c => c.CallDate.Date == date.Date)
                     .AsEnumerable()
                     .GroupBy(c => c.EndTime.Hours)
                     .Select(g => new { Hour = g.Key, Volume = g.Count() })
                     .OrderBy(g => g.Hour)
                     .ToList();

                return volumes;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting call volumes distributed by time of day from the database.", ex);
            }
        }

        public async Task<decimal> GetCostAnalysis(DateTime startDate, DateTime endDate)
        {
            try
            {
                var totalCost = await cdrContext.CallRecords
                         .Where(c => c.CallDate >= startDate && c.CallDate <= endDate)
                         .SumAsync(c => c.Cost);

                return totalCost;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting cost analysis from the database.", ex);
            }
        }

        public async Task<IEnumerable<string>> GetTopCalledNumbers()
        {
            try
            {
                var topCalledNumbers = await cdrContext.CallRecords
                        .GroupBy(c => c.Recipient)
                        .OrderByDescending(g => g.Count())
                        .Take(10)
                        .Select(g => g.Key)
                        .ToListAsync();

                return topCalledNumbers;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting top call numbers from the database.", ex);
            }
        }

        public async Task<int> GetTotalCalls(DateTime startDate, DateTime endDate)
        {
            try
            {
                var totalCalls = await cdrContext.CallRecords.CountAsync(c => c.CallDate >= startDate && c.CallDate <= endDate);
                return totalCalls;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting total call records from the database.", ex);
            }
        }

        public async Task<IEnumerable<string>> GetUnusualActivity(int threshold)
        {
            try
            {
                var unusualNumbers = await cdrContext.CallRecords
                    .GroupBy(c => c.CallerId)
                    .Where(g => g.Count() >= threshold)
                    .Select(g => g.Key)
                    .ToListAsync();

                return unusualNumbers;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while unusual ascivity in the database.", ex);
            }
        }

        public async Task ToStoreRecords(IEnumerable<CallRecord> callRecords)
        {
            try
            {
                await cdrContext.CallRecords.AddRangeAsync(callRecords);
                await cdrContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the call records to the database.", ex);
            }

        }
    }
}
