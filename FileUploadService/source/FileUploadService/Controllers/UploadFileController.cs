using FileUploadService.Attributes;
using FileUploadService.FileService;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadFileController : ControllerBase
    {
        private readonly IFileService fileService;

        public UploadFileController(IFileService fileService)
        {
            this.fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [MultipartFormData]
        [DisableFormValueModelBinding]
        public async Task<ActionResult> UploadFile()
        {
            var fileUploadSummary = await this.fileService.ProcessFileAsync(HttpContext.Request.Body, Request.ContentType);
            return Ok(fileUploadSummary);
        }

    }
}
