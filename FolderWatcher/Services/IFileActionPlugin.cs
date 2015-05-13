using System.Threading.Tasks;

namespace FolderWatcher.Services
{
    public interface IFileActionPlugin
    {
        string Name { get; }
        Task<string> Execute(string file, params object[] args);
    }
}