using MeetingSystem.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileStorageService _fileService;
        public FilesController(IFileStorageService fileStorageService)
        {
            _fileService = fileStorageService;
        }

        [HttpGet("{*filePath}")]
        public async Task<IActionResult> GetFileAsync(string filePath)
        {
            var result = await _fileService.ReturnFile(filePath);

            if (result.isFailure)
            {
                return NotFound();
            }

            return File(result.Data.FileStream, result.Data.ContentType);
        }
    }
}
