export interface FileMetadata 
{
    id:               number;
    recordTypeId:     number;
    recordTypeName:   string;
    fileName:         string;
    fileType:         string;
    fileSize:         number;
    uploadedAt:       Date;
    uploadedBy:       string;
    fileNameGuid:     string;
    uploadedFilePath: string;
}
