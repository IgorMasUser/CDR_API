using CDR_API.Data;
using CDR_API.Models;
using CDR_API.Services.Abstraction;

namespace CDR_API.Services.Impl
{
    public class RecordsStoreService : IRecordsStoreService
    {
        private readonly CdrContext cdrContext;

        public RecordsStoreService(CdrContext cdrContext)
        {
            this.cdrContext = cdrContext ?? throw new ArgumentNullException(nameof(cdrContext));
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
