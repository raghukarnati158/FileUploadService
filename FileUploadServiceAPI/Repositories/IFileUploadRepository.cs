using FileUploadServiceAPI.Models;

namespace FileUploadServiceAPI.Repositories
{
    public interface IFileUploadRepository
    {
        Task AddFileAsync(FileMetadata file);
        Task<string> UploadFileAsync(IFormFile file, int recordTypeId);
        Task<IEnumerable<FileMetadata>> GetAllFilesAsync();
        Task<IEnumerable<FileMetadata>> GetFilesByRecordTypeIdAsync(int recordTypeId);
    }
}
