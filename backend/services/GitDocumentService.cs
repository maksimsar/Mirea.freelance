using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mirea.freelance.backend.data;
using Mirea.freelance.backend.dto;

namespace Mirea.freelance.backend.services
{
    public class GitDocumentService : IGitDocumentService
    {
        private readonly GitLabClient _client;
        private const string          Branch = "main";

        public GitDocumentService(GitLabClient client)
            => _client = client;

        public async Task<IEnumerable<DocumentInfoDto>> ListAsync(string folder = null)
        {
            var files = await _client.ListFilesAsync(folder, Branch);
            var result = new List<DocumentInfoDto>();
            foreach (var f in files.Where(x => x.type == "blob"))
            {
                var commits = (await _client.ListCommitsAsync(f.path, Branch)).ToList();
                if (!commits.Any()) continue;
                result.Add(new DocumentInfoDto
                {
                    Path         = f.path,
                    FileName     = f.name,
                    VersionCount = commits.Count,
                    LastModified = commits.First().committed_date,
                    LastCommitId = commits.First().id
                });
            }
            return result;
        }
        
        public async Task<Stream> DownloadAsync(string path, string commitId = null)
        {
            var file = await _client.GetFileAsync(path, commitId ?? Branch);
            // если в GitLab нет такого файла — возвращаем null
            if (file == null)
                return null;
            var bytes = Convert.FromBase64String(file.content);
            return new MemoryStream(bytes);
        }



        public async Task UploadAsync(string path, Stream fileStream, string commitMessage)
        {
            using var ms = new MemoryStream();
            await fileStream.CopyToAsync(ms);
            var base64 = Convert.ToBase64String(ms.ToArray());

            var exists = (await _client.GetFileAsync(path, Branch)) != null;
            if (exists)
                await _client.UpdateFileAsync(path, Branch, base64, commitMessage);
            else
                await _client.CreateFileAsync(path, Branch, base64, commitMessage);
        }

        public async Task<IEnumerable<CommitDto>> HistoryAsync(string path)
        {
            var commits = await _client.ListCommitsAsync(path, Branch);
            return commits.Select(c => new CommitDto
            {
                CommitId = c.id,
                Title    = c.title,
                Date     = c.committed_date
            });
        }

        public async Task DeleteAsync(string path, string commitMessage)
        {
            await _client.DeleteFileAsync(path, Branch, commitMessage);
        }
    }
}

