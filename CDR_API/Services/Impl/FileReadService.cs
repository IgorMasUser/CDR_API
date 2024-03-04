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
        private readonly IRecordsProcessingService recordsStoreService;
        private const int BatchSize = 1000;

        public FileReadService(IRecordsProcessingService recordsStoreService)
        {
            this.recordsStoreService = recordsStoreService;
        }

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
                var batch = new List<CallRecord>(BatchSize);

                foreach (var record in records)
                {
                    batch.Add(record);
                    if (batch.Count >= BatchSize)
                    {
                        await recordsStoreService.ToStoreRecords(batch);
                        batch.Clear(); // Prepare for next batch
                    }
                }

                // Ensure any remaining records are stored
                if (batch.Any())
                {
                    await recordsStoreService.ToStoreRecords(batch);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
