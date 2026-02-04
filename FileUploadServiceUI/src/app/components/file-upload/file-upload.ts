import { Component, ElementRef, ViewChild } from '@angular/core';
import { FileUploadService } from '../../services';
import { CommonModule } from '@angular/common';
import { FileMetadata } from '../../types/file-metadata.type';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-file-upload',
  imports: [ CommonModule, FormsModule],
  templateUrl: './file-upload.html',
  styleUrl: './file-upload.scss',
})
export class FileUpload {
  constructor(private fileUploadService: FileUploadService) {}

  showScuccessAlert: boolean = false;
  showErrorAlert: boolean = false;
  successMessage: string = 'File Uploaded Successfully';
  errorMessage: string = 'Error uploading file';
  recordTypeId: number = 0;
  uploadedFiles: FileMetadata[] = [];
  filteredFiles: FileMetadata[] = [];
  recordTypeIdFilter: number = 0;
  @ViewChild('fileInput') fileInputRef!: ElementRef<HTMLInputElement>;

  ngOnInit() {
    this.getUploadedFiles();
  }

  onFileUpload(fileInput: any) {
    const file: File = fileInput.files[0];
    if (!file) {
      this.errorMessage = 'Please select a file to upload';
      this.showErrorAlert = true;
      this.autoHideAlerts();
      return;
    }

    //Check if recordTypeId is selected
    if (this.recordTypeId === 0) {
      this.errorMessage = 'Please select a Record Type to upload the file';
      this.showErrorAlert = true;
      this.autoHideAlerts();
      return;
    }    

    this.fileUploadService.uploadFile(file, this.recordTypeId)
    .subscribe({
      next: (response) => {
        //reset the form controls
        this.resetFormControls();

        this.showScuccessAlert = true;
        this.autoHideAlerts();
        this.getUploadedFiles();
      },
      error: (error) => {
        console.error('Error uploading file:', error);
        this.errorMessage = `Error uploading file: ${error.error || error}`;
        this.showErrorAlert = true;
        this.autoHideAlerts();
      }
    });
  }

  onRecordTypeChanged(selectedValue: number) {
    this.recordTypeId = selectedValue;
  }

  onRecordTypeFilterChanged(selectedValue: number) {
    this.recordTypeIdFilter = selectedValue;
    this.getUploadedFiles();
  }

  autoHideAlerts() {
    setTimeout(() => {
      this.showScuccessAlert = false;
      this.showErrorAlert = false;
    }, 5000); // Auto-hide alerts after 5 seconds
  }

  getUploadedFiles() {
    this.fileUploadService.getAllFiles()
      .subscribe({
        next: (files) => {
          this.uploadedFiles = files;
          this.filteredFiles = [];
          if(this.recordTypeIdFilter && this.recordTypeIdFilter > 0){
            this.filteredFiles = this.uploadedFiles.filter(file => file.recordTypeId === +this.recordTypeIdFilter);
          } else {
            this.filteredFiles = this.uploadedFiles;
          }          
        },
        error: (error) => {
          console.error('Error fetching uploaded files:', error);
          this.errorMessage = 'Error fetching uploaded files';
          this.showErrorAlert = true;
          this.autoHideAlerts();
        }
      });
    }

    resetFormControls(){
      //Reset RecordType dropdown
      var recordTypeDropDown = document.getElementById("recordtype") as HTMLSelectElement;
      if (recordTypeDropDown) {
        this.recordTypeId = 0;
        recordTypeDropDown.selectedIndex = 0;
      }

      //Reset file input control
      if (this.fileInputRef?.nativeElement) {
        this.fileInputRef.nativeElement.value = '';
      }
    }
  }