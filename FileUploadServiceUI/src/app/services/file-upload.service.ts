import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FileMetadata } from '../types/file-metadata.type';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FileUploadService {
  constructor(private http: HttpClient) {}
  baseUrl = `http://localhost:5161`;

  uploadFile(file: File, recordTypeId: number) {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);
    const url = `${this.baseUrl}/api/FileUpload/UploadFile?recordTypeId=${recordTypeId}`;
    return this.http.post(url, formData, { responseType: 'text' });
  }

  getFilesByRecordTypeId(recordTypeId: number): Observable<FileMetadata[]> {
    const url = `${this.baseUrl}/api/FileUpload/GetFilesByRecordTypeId?recordTypeId=${recordTypeId}`;
    return this.http.get<FileMetadata[]>(url);
  }

  getAllFiles(): Observable<FileMetadata[]> {
    const url = `${this.baseUrl}/api/FileUpload/GetAllFiles`;
    return this.http.get<FileMetadata[]>(url);
  }
}
