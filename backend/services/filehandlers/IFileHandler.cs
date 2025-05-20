using System.Threading.Tasks;

namespace Mirea.freelance.backend.services.filehandlers
{
    public interface IFileHandler
    {
        Task RenderAsync(string path, object model);
        Task ValidateAsync(string path);
        Task SaveAsync(string path, string content);
    }
}
