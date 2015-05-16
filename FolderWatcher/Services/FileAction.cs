using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Services
{
    public class FileAction
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IFileActionPlugin[] _actionPlugins;
        private readonly string _file;

        public FileAction(IFileSystemService fileSystemService, IFileActionPlugin[] actionPlugins, string file)
        {
            _fileSystemService = fileSystemService;
            _actionPlugins = actionPlugins;
            _file = file;
        }

        public Task<string> Call(string action, params object[] args)
        {
            return _actionPlugins.First(o=>o.Name == action).Execute(_file, args);
        }
    }
}