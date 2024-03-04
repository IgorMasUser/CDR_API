using CDR_API.Models;
using CDR_API.Services.Abstraction;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using CDR_API.Data;

namespace CDR_API.Services.Impl
{
    public class FileReadService : IFileReadService
    {
        public async Task ToReadFile(UploadFileModel file)
        {
            try
            {
                using var reader = new StreamReader(file.FileDetails.OpenReadStream());

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true
                };

                using var csv = new CsvReader(reader, config);
                csv.Context.RegisterClassMap<CallRecordMap>();

                var records = csv.GetRecords<CallRecord>();

                foreach (var record in records)
                {
                    Console.WriteLine($"{record.CallerId} + {record.Recipient}+ {record.CallDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}" +
                        $"+ {record.EndTime}+ {record.Duration}+ {record.Cost}+ {record.Reference}+ {record.Currency}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
