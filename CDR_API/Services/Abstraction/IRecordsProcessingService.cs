using CDR_API.Models;

namespace CDR_API.Services.Abstraction
{
    public interface IRecordsProcessingService
    {
        Task ToStoreRecords(IEnumerable<CallRecord> callRecords);
        Task<int> GetTotalCalls(DateTime startDate, DateTime endDate);
        Task<double> GetAverageCallDuration(DateTime startDate, DateTime endDate);
        Task<IEnumerable<string>> GetTopCalledNumbers();
        IEnumerable<dynamic> GetCallVolumeByTimeOfDay(DateTime date);
    }
}
