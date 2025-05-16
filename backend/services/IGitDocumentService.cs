using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Mirea.freelance.backend.dto;

namespace Mirea.freelance.backend.services
{
    public interface IGitDocumentService
    {
        Task<IEnumerable<DocumentInfoDto>> ListAsync(string folder = null);
        Task<Stream>                      DownloadAsync(string path, string commitId = null);
        Task                               UploadAsync(string path, Stream fileStream, string commitMessage);
        Task<IEnumerable<CommitDto>>      HistoryAsync(string path);
        Task                               DeleteAsync(string path, string commitMessage);
        // в backend/services/IGitDocumentService.cs
        Task CreateFolderAsync(string path);
        Task RenameAsync(string oldPath, string newPath);
        Task MoveAsync(string oldPath, string newPath);
        Task<StatsDto> StatsAsync(string folder = null);

    }
}
