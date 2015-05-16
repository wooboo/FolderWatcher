using System;
using System.Threading.Tasks;
using FolderWatcher.Services;
using FolderWatcher.Services.Events;
using FolderWatcher.Watcher;

namespace FolderWatcher.Plugins.Delete
{
    public class DeletePlugin : PluginBase<DeletePluginConfig>
    {
        private readonly IFileSystemService _fileSystemService;

        public DeletePlugin(DeletePluginConfig config, IFileSystemService fileSystemService) : base(config)
        {
            _fileSystemService = fileSystemService;
        }


        public override void OnFileCreated(FileChangeInfo file)
        {
            _fileSystemService.ForFile(file.FullPath).Call("delete");
        }

    }
}