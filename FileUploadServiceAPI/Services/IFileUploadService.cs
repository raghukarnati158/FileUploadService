using FileUploadServiceAPI.Models;

namespace FileUploadServiceAPI.Services
{
    public interface IFileUploadService
    {
        Task AddFileAsync(FileMetadata file);
        Task<string> UploadFileAsync(IFormFile file, string recordTypeId);
        Task<IEnumerable<FileMetadata>> GetAllFilesAsync();
        Task<IEnumerable<FileMetadata>> GetFilesByRecordTypeIdAsync(int recordTypeId);
    }
}
