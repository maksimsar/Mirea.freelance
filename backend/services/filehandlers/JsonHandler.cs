using System.Threading.Tasks;
using System.Text.Json;
using Mirea.freelance.backend.Infrastructure;  // папка Infrastructure

namespace Mirea.freelance.backend.services.filehandlers
{
    public class JsonHandler : IFileHandler
    {
        public Task RenderAsync(string path, object model)
        {
            _ = JsonSerializer.Serialize(model);
            return Task.CompletedTask;
        }

        // Сигнатура теперь совпадает с интерфейсом
        public Task ValidateAsync(string path)
        {
            return Task.CompletedTask;
        }

        public async Task SaveAsync(string path, string content)
        {
            const string branch = "main";
            var client = GitLabClientSingleton.Instance;
            var exists = await client.GetFileAsync(path, branch) != null;
            if (exists)
                await client.UpdateFileAsync(path, branch, content, $"Save JSON {path}");
            else
                await client.CreateFileAsync(path, branch, content, $"Save JSON {path}");
        }
    }
}
