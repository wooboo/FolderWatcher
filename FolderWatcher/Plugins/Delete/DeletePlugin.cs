using System;
using System.Threading.Tasks;
using FolderWatcher.Services;
using FolderWatcher.Services.Events;
using FolderWatcher.Watcher;

namespace FolderWatcher.Plugins.Delete
{
    public class DeletePlugin : IPlugin
    {
        private readonly DeletePluginConfig _config;
        private readonly IFileSystemService _fileSystemService;

        public DeletePlugin(DeletePluginConfig config, IFileSystemService fileSystemService)
        {
            _config = config;
            _fileSystemService = fileSystemService;
        }

        public void OnFileCreated(FileChangeInfo file)
        {
            _fileSystemService.ForFile(file.FullPath).Call("delete");
        }

        public void OnFileDeleted(FileChangeInfo file)
        {
            
        }
    }
}