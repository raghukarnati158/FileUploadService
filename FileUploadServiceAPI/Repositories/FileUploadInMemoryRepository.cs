using FileUploadServiceAPI.Models;

namespace FileUploadServiceAPI.Repositories
{
    public class FileUploadInMemoryRepository : IFileUploadRepository
    {
        private readonly List<FileMetadata> _files = new();
        public async Task AddFileAsync(FileMetadata file)
        {
            _files.Add(file);
        }

        public async Task<string> UploadFileAsync(IFormFile file, int recordTypeId)
        {
            try
            {
                string fileNameGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
                if (!Directory.Exists(uploadFilePath))
                {
                    Directory.CreateDirectory(uploadFilePath);
                }
                using FileStream fileStream = new FileStream(Path.Combine(uploadFilePath, fileNameGuid), FileMode.Create);
                await file.CopyToAsync(fileStream);
                AddFileToInMemoryList(file, fileNameGuid, uploadFilePath, recordTypeId);
                return $"File uploaded successfully with GUID: {fileNameGuid}";
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while uploading the file. {ex.Message}");

            }
        }

        public async Task<IEnumerable<FileMetadata>> GetAllFilesAsync()
        {
            return _files.OrderBy(x => x.UploadedAt).ToList();
        }

        public async Task<IEnumerable<FileMetadata>> GetFilesByRecordTypeIdAsync(int recordTypeId)
        {
            return _files.Where(f => f.RecordTypeId == recordTypeId);
        }

        #region Private Methods
        private void AddFileToInMemoryList(IFormFile file, string fileNameGuid, string uploadedFilePath, int recordTypeId)
        {
            var uploadedFile = new FileMetadata
            {
                Id = _files.Count + 1,
                RecordTypeId = recordTypeId,
                RecordTypeName = ((Enums.RecordType)recordTypeId).ToString(), 
                FileName = file.FileName,
                FileType = Path.GetExtension(file.FileName),
                FileSize = file.Length,
                UploadedAt = DateTime.UtcNow,
                UploadedBy = "Raghu Karnati", //Improvements
                FileNameGuid = fileNameGuid,
                UploadedFilePath = uploadedFilePath
            };

            _files.Add(uploadedFile);
        }
        #endregion
    }
}
