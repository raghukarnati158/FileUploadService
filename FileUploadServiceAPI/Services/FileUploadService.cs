using FileUploadServiceAPI.Models;
using FileUploadServiceAPI.Repositories;
using static FileUploadServiceAPI.Models.Enums;

namespace FileUploadServiceAPI.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IFileUploadRepository _fileUploadRepository;

        public FileUploadService(IFileUploadRepository fileUploadRepository)
        {
            _fileUploadRepository = fileUploadRepository;
        }

        public async Task AddFileAsync(FileMetadata file)
        {
            try
            {
                await _fileUploadRepository.AddFileAsync(file);
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while adding the file metadata. {ex.Message}");
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file, string recordTypeId)
        {
            int _recordTypeId = (int)RecordType.PortfolioCompanies; // Default RecordTypeId
            if (!string.IsNullOrEmpty(recordTypeId) && int.TryParse(recordTypeId, out int recordTypeIdInt))
            {
                _recordTypeId = Convert.ToInt32(recordTypeId); // Default RecordTypeId
            }

            //Do any validations here
            List<string> allowedExtensions = new List<string> { ".jpg", ".png", ".pdf", ".docx", ".doc", ".xlsx" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new Exception("File type is not allowed.");
            }

            //Check FileSize (e.g., max 5MB)
            long maxFileSize = 5 * 1024 * 1024; // 5MB
            if (file.Length > maxFileSize)
            {
                throw new Exception("File size exceeds the maximum limit of 5MB.");
            }

            try
            {
                var response = await _fileUploadRepository.UploadFileAsync(file, _recordTypeId);
                return response;
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while uploading the file. {ex.Message}");
            }
        }

        public async Task<IEnumerable<FileMetadata>> GetAllFilesAsync()
        {
            try
            {
                return await _fileUploadRepository.GetAllFilesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while retrieving all files. {ex.Message}");
            }
        }

        public async Task<IEnumerable<FileMetadata>> GetFilesByRecordTypeIdAsync(int recordTypeId)
        {
            try
            {
                if(recordTypeId == 0)
                {
                    return await _fileUploadRepository.GetAllFilesAsync();
                }
                else
                {
                    return await _fileUploadRepository.GetFilesByRecordTypeIdAsync(recordTypeId);
                }                    
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while retrieving files for record Id {recordTypeId}. {ex.Message}");
            }
        }
    }
}
