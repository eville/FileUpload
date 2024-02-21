using FileUploadService.Models;

namespace FileUploadService.FileService
{
    public interface IFileService
    {
        Task<FileUploadSummary> ProcessFileAsync(Stream fileStream, string contentType);
    }
}