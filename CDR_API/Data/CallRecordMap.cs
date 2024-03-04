using CDR_API.Models;
using CDR_API.Utils;
using CsvHelper.Configuration;
using System.Globalization;

namespace CDR_API.Data
{
    public class CallRecordMap : ClassMap<CallRecord>
    {
        public CallRecordMap()
        {
            Map(m => m.CallerId).Name("caller_id");
            Map(m => m.Recipient).Name("recipient");
            Map(m => m.CallDate).Convert(args =>
            {
                var callDateAsString = args.Row.GetField("call_date");
                return DateTime.ParseExact(callDateAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            });
            Map(m => m.EndTime).Name("end_time").TypeConverter<TimeSpanConverter>();
            Map(m => m.Duration).Name("duration");
            Map(m => m.Cost).Name("cost");
            Map(m => m.Reference).Name("reference");
            Map(m => m.Currency).Name("currency");
        }
    }
}
