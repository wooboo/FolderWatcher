using System.ComponentModel.Composition;
using System.IO;
using FolderWatcher.Services;
using FolderWatcher.Watcher;
using Newtonsoft.Json;

namespace FolderWatcher.Plugins.DeleteFile
{
    [Export(typeof(IPluginFactory))]
    public class DeleteFilePluginFactory : PluginFactoryBase<DeleteFilePlugin, DeleteFilePluginConfig>
    {
        private readonly IFileSystemService _fileSystemService;
        [ImportingConstructor]
        public DeleteFilePluginFactory(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }

        protected override DeleteFilePluginConfig CreateConfig(string path)
        {
            return new DeleteFilePluginConfig(path);
        }

        protected override DeleteFilePlugin CreatePlugin(DeleteFilePluginConfig config)
        {
            return new DeleteFilePlugin(config, _fileSystemService);
        }
    }
}