using CDR_API.Models;
using CDR_API.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CDR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CallRecordsController : ControllerBase
    {
        private readonly IFileReadService fileReadService;

        public CallRecordsController(IFileReadService fileReadService)
        {
            this.fileReadService = fileReadService;
        }

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
               await fileReadService.ToReadFile(file);
               return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
