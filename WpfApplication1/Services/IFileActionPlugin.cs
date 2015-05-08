using System.Threading.Tasks;

namespace FolderWatcher.Services
{
    public interface IFileActionPlugin
    {
        Task<string> Execute(string file, params string[] args);
    }
}