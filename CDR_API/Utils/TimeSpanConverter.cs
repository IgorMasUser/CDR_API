using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace CDR_API.Utils
{
    public class TimeSpanConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (TimeSpan.TryParseExact(text, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out TimeSpan result))
            {
                return result;
            }

            throw new TypeConverterException(this, memberMapData, text, row.Context, "The provided time string is not in the expected format.");
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is TimeSpan timeSpan)
            {
                return timeSpan.ToString(@"hh\:mm\:ss");
            }

            return base.ConvertToString(value, row, memberMapData);
        }
    }

}
