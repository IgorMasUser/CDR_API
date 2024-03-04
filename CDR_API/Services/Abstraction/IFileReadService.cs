using CDR_API.Models;

namespace CDR_API.Services.Abstraction
{
    public interface IFileReadService
    {
        Task ToReadFile(UploadFileModel file);
    }
}
