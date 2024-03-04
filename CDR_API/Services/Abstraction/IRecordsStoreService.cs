using CDR_API.Models;

namespace CDR_API.Services.Abstraction
{
    public interface IRecordsStoreService
    {
        Task<CallRecord> ToStoreRecords(CallRecord callRecord);
    }
}
