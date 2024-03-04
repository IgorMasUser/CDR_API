using CDR_API.Models;

namespace CDR_API.Services.Abstraction
{
    public interface IRecordsStoreService
    {
        Task ToStoreRecords(IEnumerable<CallRecord> callRecords);
    }
}
