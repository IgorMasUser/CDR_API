using CDR_API.Models;
using CDR_API.Services.Abstraction;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using CDR_API.Data;
using CDR_API.Configs;
using CDR_API.Utils;

namespace CDR_API.Services.Impl
{
    public class FileReadService : IFileReadService
    {
        private readonly IRecordsProcessingService recordsStoreService;
        private readonly ILogger<FileReadService> logger;
        private readonly int batchSize;
        private CallRecordValidator validator = new CallRecordValidator();

        public FileReadService(IRecordsProcessingService recordsStoreService,
            FileReadServiceOptions options,
            ILogger<FileReadService> logger)
        {
            this.recordsStoreService = recordsStoreService;
            this.logger = logger;
            this.batchSize = options.BatchSize;
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
                var batch = new List<CallRecord>(batchSize);

                foreach (var record in records)
                {
                    var validationResult = validator.Validate(record);
                    if (!validationResult.IsValid)
                    {
                        foreach (var error in validationResult.Errors)
                        {
                            logger.LogError($"Record with values: {record.CallerId};{record.Recipient};{record.Reference};{record.CallDate} is invalid: {error.ErrorMessage}");
                        }
                        continue;
                    }

                    batch.Add(record);
                    if (batch.Count >= batchSize)
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
