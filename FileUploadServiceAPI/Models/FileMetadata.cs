namespace FileUploadServiceAPI.Models
{
    public class FileMetadata
    {
        public int Id { get; set; }
        public int RecordTypeId { get; set; }
        public string RecordTypeName { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long? FileSize { get; set; }
        public DateTime? UploadedAt { get; set; } //UTC DateTime
        public string UploadedBy { get; set; } = string.Empty; //Improvements
        public string FileNameGuid { get; set; } = string.Empty;
        public string UploadedFilePath { get; set; } = string.Empty;
    }
}
