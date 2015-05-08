using System.Windows.Input;
using FolderWatcher.Services;
using FolderWatcher.Watcher;
using Microsoft.Practices.Prism.Commands;

namespace FolderWatcher.Plugins.DeleteFile
{
    public class DeleteFilePluginPart : IPluginPart {
        private readonly IFileSystemService _fileSystemService;
        public ChangedFile File { get; private set; }

        public DeleteFilePluginPart(IFileSystemService fileSystemService, ChangedFile file)
        {
            _fileSystemService = fileSystemService;
            File = file;
            Delete = new DelegateCommand<ChangedFile>(ExecuteMethod);
        }

        private async void ExecuteMethod(ChangedFile changedFile)
        {
            await _fileSystemService.ForFile(changedFile).Call("delete");
        }

        public ICommand Delete { get; set; }
    }
}