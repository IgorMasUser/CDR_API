using CDR_API.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CDR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CallRecordsController : ControllerBase
    {

        /// <summary>
        /// File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
      
        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile([FromForm] UploadFileModel file)
        {
            if (file == null)
            {
                return BadRequest();
            }

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

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
