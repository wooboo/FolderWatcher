using System.Threading.Tasks;
using FolderWatcher.Watcher;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Services
{
    public class FileAction
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly ChangedFile _file;

        public FileAction(IFileSystemService fileSystemService, ChangedFile file)
        {
            _fileSystemService = fileSystemService;
            _file = file;
        }

        public Task<string> Call(string action, params string[] args)
        {
            return ServiceLocator.Current.GetInstance<IFileActionPlugin>(action).Execute(_file.FullPath, args);
        }
    }
}