using FileUploadServiceAPI.Models;

namespace FileUploadServiceAPI.Repositories
{
    public class FileUploadToAzureBlobRepository : IFileUploadRepository
    {
        public async Task AddFileAsync(FileMetadata file)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FileMetadata>> GetAllFilesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FileMetadata>> GetFilesByRecordTypeIdAsync(int recordTypeId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFileAsync(IFormFile file, int recordTypeId)
        {
            throw new NotImplementedException();
        }
    }
}
