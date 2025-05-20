using System;
using System.IO;
using System.Threading.Tasks;
using Mirea.freelance.backend.Infrastructure;

namespace Mirea.freelance.backend.services.filehandlers
{
    /// <summary>
    /// Обработчик для простых текстовых файлов (.txt и т.п.)
    /// </summary>
    public class TextHandler : IFileHandler
    {
        public Task RenderAsync(string path, object model)
        {
            // Ничего не рендерим для plain-text
            return Task.CompletedTask;
        }

        public Task ValidateAsync(string path)
        {
            // Можно добавить проверку кодировки или размера,
            // но по умолчанию пропускаем
            return Task.CompletedTask;
        }

        public async Task SaveAsync(string path, string content)
        {
            const string branch = "main";
            var client = GitLabClientSingleton.Instance;

            // GitLabClient.GetFileAsync возвращает null, если файла нет
            var exists = await client.GetFileAsync(path, branch) != null;

            if (exists)
                await client.UpdateFileAsync(path, branch, content, $"Save text {path}");
            else
                await client.CreateFileAsync(path, branch, content, $"Create text {path}");
        }
    }
}
