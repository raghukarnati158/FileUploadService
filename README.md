# FileUploadService
This service allows user to upload files from the UI and these files will be uploaded to SQL or Azure Blobs 

**RUNNING THE APPLICATION LOCALLY**
**PREREQUISITES**
•	.NET 10 SDK (or compatible)
•	Node.js (v22+ recommended)
•	Npm – 11.7.0
•	Angular CLI – Angular 20

**BACKEND (API)**
•	Download the Frontend and Backend projects from the GIT repository located at https://github.com/raghukarnati158/FileUploadService 
•	Open Command prompt (with Admin rights) and navigate to the folder where the slnx file is located. For example:
cd \GIT\FileUploadService\FileUploadServiceUI
•	Run this command first: dotnet restore
•	And then run this command: dotnet run
•	The API will start on:
https://localhost:5161
•	Uploaded files will be saved to:
/UploadedFiles

**FRONTEND (ANGULAR)**
•	Open another command prompt (with Admin rights) and navigate to the UI project folder where the package.json is located. For example:
cd \GIT\FileUploadService\FileUploadServiceUI
•	npm install
•	Now check if the API Url is configured correctly in the UI project. Go to this file: \GIT\FileUploadService\FileUploadServiceUI\src\app\services\file-upload.service.ts
•	At line number 11, update the port number for the baseUrl variable if necessary. This baseUrl should match the WebApi Url that is running locally. You can get this UR from the Command Prompt where you are running the WebApi
•	ng serve --open
•	The UI will be available at:
•	http://localhost:4200
•	You should see the UI application running and also talking to the WebApi for File upload functionality.

