using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mirea.freelance.backend.data;               // для GitLabClient
using Mirea.freelance.backend.dto;                // для DTO
using Mirea.freelance.backend.Infrastructure;                  // <- здесь
using Mirea.freelance.backend.services.Models;                // для DocumentNode/Tree
using Mirea.freelance.backend.services.filehandlers;   // для Singleton-обёртки
using Mirea.freelance.backend.services;           // для Builder (DocumentTreeBuilder)
// для FileHandlerFactory

namespace Mirea.freelance.backend.services
{
    public class GitDocumentService : IGitDocumentService
    {
        private readonly GitLabClient _client;
        private const string Branch = "main";

        // Конструктор по-умолчанию, использующий Singleton
        public GitDocumentService()
            : this(GitLabClientSingleton.Instance)
        { }

        // Оригинальный конструктор для DI
        public GitDocumentService(GitLabClient client)
            => _client = client;

        public async Task CreateFolderAsync(string path)
        {
            var folder = path.TrimEnd('/');
            var keepPath = $"{folder}/.gitkeep";
            await _client.CreateFileAsync(keepPath, Branch, "", $"Create folder {folder}");
        }

        public async Task RenameAsync(string oldPath, string newPath)
        {
            using var stream = await DownloadAsync(oldPath);
            await UploadAsync(newPath, stream, $"Rename {oldPath} to {newPath}");
            await DeleteAsync(oldPath, $"Delete old {oldPath} after rename");
        }

        public async Task MoveAsync(string oldPath, string newPath)
            => await RenameAsync(oldPath, newPath);

        public async Task<StatsDto> StatsAsync(string folder = null)
        {
            var list = await ListAsync(folder);
            return new StatsDto
            {
                TotalFiles    = list.Count(),
                TotalVersions = list.Sum(d => d.VersionCount),
                TotalBytes    = 0,
                RecentCommits = 0
            };
        }

        public async Task<IEnumerable<DocumentInfoDto>> ListAsync(string folder = null)
        {
            // получаем и файлы, и папки из GitLab
            var items = await _client.ListFilesAsync(folder, Branch);
            var result = new List<DocumentInfoDto>();

            foreach (var f in items)
            {
                if (f.type == "tree")
                {
                    // папка
                    result.Add(new DocumentInfoDto {
                        Path         = f.path,
                        FileName     = f.name,
                        Type         = "tree",
                        VersionCount = 0,
                        LastModified = DateTime.MinValue,
                        LastCommitId = ""
                    });
                }
                else if (f.type == "blob")
                {
                    // файл
                    var commits = (await _client.ListCommitsAsync(f.path, Branch)).ToList();
                    if (!commits.Any()) continue;
                    result.Add(new DocumentInfoDto
                    {
                        Path         = f.path,
                        FileName     = f.name,
                        Type         = "blob",
                        VersionCount = commits.Count,
                        LastModified = commits.First().committed_date,
                        LastCommitId = commits.First().id
                    });
                }
            }

            return result;
        }


        public async Task<Stream> DownloadAsync(string path, string commitId = null)
        {
            var file = await _client.GetFileAsync(path, commitId ?? Branch);
            if (file == null) return null;
            var bytes = Convert.FromBase64String(file.content);
            return new MemoryStream(bytes);
        }

        public async Task UploadAsync(string path, Stream fileStream, string commitMessage)
        {
            using var ms = new MemoryStream();
            await fileStream.CopyToAsync(ms);
            var base64 = Convert.ToBase64String(ms.ToArray());

            // Abstract Factory для хэндлеров
            var ext     = Path.GetExtension(path);
            var handler = FileHandlerFactory.Create(ext);

            await handler.ValidateAsync(path);
            await handler.SaveAsync(path, base64);
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

        // Пример использования Builder
        public async Task InitializeStructureAsync()
        {
            var tree = new DocumentTreeBuilder()
                .AddFolder("docs")
                .AddFile("docs/README.md", template: "default")
                .Build();

            foreach (var node in tree.Nodes)
            {
                if (node.Type == NodeType.Folder)
                    await CreateFolderAsync(node.Path);
                else
                    await UploadAsync(node.Path, new MemoryStream(), $"Init {node.Path}");
            }
        }
    }
}
