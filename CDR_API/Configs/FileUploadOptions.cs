namespace CDR_API.Configs
{
    public class FileUploadOptions
    {
        public long MaxRequestBodySize { get; set; }
        public long MultipartBodyLengthLimit { get; set; }
    }
}
