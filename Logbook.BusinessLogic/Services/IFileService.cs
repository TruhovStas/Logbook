using Microsoft.AspNetCore.Http;

namespace EventsWeb.BusinessLogic.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions,
            CancellationToken cancellationToken);
        void DeleteFile(string fileNameWithExtension);
    }
}
