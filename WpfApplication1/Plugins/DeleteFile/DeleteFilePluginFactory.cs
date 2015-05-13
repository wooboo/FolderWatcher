using System.ComponentModel.Composition;
using System.IO;
using FolderWatcher.Services;
using FolderWatcher.Watcher;
using Newtonsoft.Json;

namespace FolderWatcher.Plugins.DeleteFile
{
    [Export(typeof(IPluginFactory))]
    public class DeleteFilePluginFactory : IPluginFactory
    {
        private readonly IFileSystemService _fileSystemService;
        [ImportingConstructor]
        public DeleteFilePluginFactory(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }

        public string Name => typeof(DeleteFilePlugin).Name;

        public bool TryCreatePlugin(string path, out IPlugin plugin)
        {
            var pluginConfig = Path.Combine(path, Name);
            var config = new DeleteFilePluginConfig(pluginConfig);
            if (config.TryLoad())
            {
                plugin = new DeleteFilePlugin(config,_fileSystemService);
                return true;
            }
            plugin = null;
            return false;
        }

    }
}