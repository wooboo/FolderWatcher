using System.ComponentModel.Composition;

namespace FolderWatcher.Services
{
    [Export(typeof (IFileSystemService))]
    public class FileSystemService : IFileSystemService
    {
        private readonly IFileActionPlugin[] _fileActionPlugins;
        [ImportingConstructor]
        public FileSystemService([ImportMany] IFileActionPlugin[] fileActionPlugins)
        {
            _fileActionPlugins = fileActionPlugins;
        }

        public FileAction ForFile(string file)
        {
            return new FileAction(this, _fileActionPlugins, file);
        }
    }
}
