using System.IO;
using Microsoft.AspNetCore.Mvc;
using Mirea.freelance.backend.services;

namespace Mirea.freelance.backend.controllers
{
    [ApiController]
    [Route("api/gitdocs")]
    public class GitDocsController : ControllerBase
    {
        private readonly IGitDocumentService _svc;
        public GitDocsController(IGitDocumentService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] string folder = null)
            => Ok(await _svc.ListAsync(folder));
        [HttpGet("{*path}")]
        public async Task<IActionResult> Download(string path, [FromQuery] string commitId = null)
        {
            var stream = await _svc.DownloadAsync(path, commitId);
            if (stream == null)
                return NotFound();         // вместо 500 отдаём корректный 404
            return File(stream,
                        "application/octet-stream",
                        Path.GetFileName(path));
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] UploadFileDto dto)
        {
            using var stream = dto.File.OpenReadStream();
            await _svc.UploadAsync(dto.Path ?? dto.File.FileName, stream, $"Upload {dto.File.FileName}");
            return Ok();
        }


        [HttpGet("history/{*path}")]
        public async Task<IActionResult> History(string path)
            => Ok(await _svc.HistoryAsync(path));

        [HttpDelete("{*path}")]
        public async Task<IActionResult> Delete(string path)
        {
            await _svc.DeleteAsync(path, $"Delete {path}");
            return NoContent();
        }
    }
}
