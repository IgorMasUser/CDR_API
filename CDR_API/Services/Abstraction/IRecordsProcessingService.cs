using CDR_API.Models;

namespace CDR_API.Services.Abstraction
{
    public interface IRecordsProcessingService
    {
        Task ToStoreRecords(IEnumerable<CallRecord> callRecords);
        Task<int> GetTotalCalls(DateTime startDate, DateTime endDate);
    }
}
