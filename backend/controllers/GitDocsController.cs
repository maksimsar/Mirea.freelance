using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mirea.freelance.backend.services;

namespace Mirea.freelance.backend.controllers
{
    [Authorize]
    [ApiController]
    [Route("api/gitdocs")]
    public class GitDocsController : ControllerBase
    {
        private readonly IGitDocumentService _svc;
        public GitDocsController(IGitDocumentService svc) => _svc = svc;

        [HttpGet]
        [Authorize(Roles = "Student,Company,Mentor,Admin")]
        public async Task<IActionResult> List([FromQuery] string folder = null)
            => Ok(await _svc.ListAsync(folder));

        [HttpGet("{*path}")]
        [Authorize(Roles = "Student,Company,Mentor,Admin")]
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
        [Authorize(Roles = "Student,Mentor,Admin")]
        public async Task<IActionResult> Upload([FromForm] UploadFileDto dto)
        {
            using var stream = dto.File.OpenReadStream();
            await _svc.UploadAsync(dto.Path ?? dto.File.FileName, stream, $"Upload {dto.File.FileName}");
            return Ok();
        }


        [HttpGet("history/{*path}")]
        [Authorize(Roles = "Student,Mentor,Admin, Company")]
        public async Task<IActionResult> History(string path)
            => Ok(await _svc.HistoryAsync(path));

        [HttpDelete("{*path}")]
        [Authorize(Roles = "Admin, Mentor")]
        public async Task<IActionResult> Delete(string path)
        {
            await _svc.DeleteAsync(path, $"Delete {path}");
            return NoContent();
        }

        [HttpGet("stats")]
        [Authorize(Roles = "Company,Mentor,Admin")]
        public async Task<IActionResult> Stats([FromQuery] string folder = null)
        {
            var s = await _svc.StatsAsync(folder);
            return Ok(s);
        }

        // Создать папку
        [HttpPost("folder")]
        [Authorize(Roles = "Student,Mentor,Admin")]
        public async Task<IActionResult> CreateFolder([FromQuery] string path)
        {
            await _svc.CreateFolderAsync(path);
            return Ok();
        }

        // Переименовать (путь к пути)
        [HttpPost("rename")]
        [Authorize(Roles = "Student,Mentor,Admin")]
        public async Task<IActionResult> Rename([FromQuery] string oldPath, [FromQuery] string newPath)
        {
            await _svc.RenameAsync(oldPath, newPath);
            return Ok();
        }

        // Переместить (аналогично rename)
        [HttpPost("move")]
        [Authorize(Roles = "Student,Mentor,Admin")]
        public async Task<IActionResult> Move([FromQuery] string oldPath, [FromQuery] string newPath)
        {
            await _svc.MoveAsync(oldPath, newPath);
            return Ok();
        }
    }


}

