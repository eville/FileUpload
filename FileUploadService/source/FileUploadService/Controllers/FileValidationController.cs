using FileUploadService.Attributes;
using FileUploadService.FileService;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadService.Controllers
{
    [ApiController]
    [Route("api")]
    public class FileValidationController : ControllerBase
    {
        private readonly IFileService fileService;

        public FileValidationController(IFileService fileService)
        {
            this.fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        [HttpPost("validatefile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [MultipartFormData]
        [DisableFormValueModelBinding]
        public async Task<ActionResult> ValidateFile()
        {
            var fileUploadSummary = await this.fileService.ProcessFileAsync(HttpContext.Request.Body, Request.ContentType);
            return Ok(fileUploadSummary);
        }

    }
}
