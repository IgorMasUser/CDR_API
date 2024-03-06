namespace CDR_API.Models
{
    public class FileUploadOptions
    {
        public long MaxRequestBodySize { get; set; }
        public long MultipartBodyLengthLimit { get; set; }
    }
}
