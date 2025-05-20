using System;
using System.IO;
using System.Threading.Tasks;
using Mirea.freelance.backend.Infrastructure;

namespace Mirea.freelance.backend.services.filehandlers
{
    /// <summary>
    /// Обработчик для двоичных файлов (PDF, Word, Excel и т.п.)
    /// Контент передаётся как Base64-строка.
    /// </summary>
    public class BinaryHandler : IFileHandler
    {
        public Task RenderAsync(string path, object model)
        {
            // Для двоичных файлов рендер не нужен
            return Task.CompletedTask;
        }

        public Task ValidateAsync(string path)
        {
            // Здесь можно, например, проверить размер файла
            return Task.CompletedTask;
        }

        public async Task SaveAsync(string path, string content)
        {
            const string branch = "main";
            var client = GitLabClientSingleton.Instance;

            var exists = await client.GetFileAsync(path, branch) != null;
            if (exists)
                await client.UpdateFileAsync(path, branch, content, $"Save binary {path}");
            else
                await client.CreateFileAsync(path, branch, content, $"Create binary {path}");
        }
    }
}
