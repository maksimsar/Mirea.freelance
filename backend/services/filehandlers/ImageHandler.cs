using System.Threading.Tasks;
using Mirea.freelance.backend.Infrastructure;  // правильный namespace для вашего Singelton

namespace Mirea.freelance.backend.services.filehandlers
{
    public class ImageHandler : IFileHandler
    {
        public Task RenderAsync(string path, object model)
        {
            // Здесь можно, например, генерировать миниатюры
            return Task.CompletedTask;
        }

        public Task ValidateAsync(string path)
        {
            // Проверка расширения или других метаданных
            return Task.CompletedTask;
        }

        public async Task SaveAsync(string path, string content)
        {
            const string branch = "main";
            var client = GitLabClientSingleton.Instance;
            var exists = await client.GetFileAsync(path, branch) != null;

            if (exists)
                await client.UpdateFileAsync(path, branch, content, $"Save image {path}");
            else
                await client.CreateFileAsync(path, branch, content, $"Save image {path}");
        }
    }
}
