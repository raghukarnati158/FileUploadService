using FileUploadServiceAPI.Models;
using FileUploadServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using static FileUploadServiceAPI.Models.Enums;

namespace FileUploadServiceAPI.Controllers
{
    [ApiController]
    [Route("api/fileupload")]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;

        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost("UploadFile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFileAsync(IFormFile file, string recordTypeId)
        {
            if (file == null)
            {
                return BadRequest("No File Found.");
            }

            try
            {
                var response = await _fileUploadService.UploadFileAsync(file, recordTypeId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllFiles")]
        public async Task<IActionResult> GetAllFilesAsync()
        {
            try
            {
                var files = await _fileUploadService.GetAllFilesAsync();
                return Ok(files);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetFilesByRecordTypeId")]
        public async Task<IActionResult> GetFilesByRecordTypeIdAsync(int recordTypeId)
        {
            try
            {
                var files = await _fileUploadService.GetFilesByRecordTypeIdAsync(recordTypeId);
                return Ok(files);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, ex.Message);
            }
        }
    }
}
