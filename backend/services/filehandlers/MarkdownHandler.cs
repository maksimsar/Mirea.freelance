using System.Threading.Tasks;
using Mirea.freelance.backend.Infrastructure;

namespace Mirea.freelance.backend.services.filehandlers
{
    public class MarkdownHandler : IFileHandler
    {
        public Task RenderAsync(string path, object model)
        {
            // ваш код рендера MD
            return Task.CompletedTask;
        }

        public Task ValidateAsync(string path)
        {
            // валидация MD, если нужно
            return Task.CompletedTask;
        }

        public async Task SaveAsync(string path, string content)
        {
            const string branch = "main";
            var client = GitLabClientSingleton.Instance;
            var exists = await client.GetFileAsync(path, branch) != null;
            if (exists)
                await client.UpdateFileAsync(path, branch, content, $"Save MD {path}");
            else
                await client.CreateFileAsync(path, branch, content, $"Save MD {path}");
        }
    }
}
