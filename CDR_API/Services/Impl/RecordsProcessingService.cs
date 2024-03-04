using CDR_API.Data;
using CDR_API.Models;
using CDR_API.Services.Abstraction;
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
