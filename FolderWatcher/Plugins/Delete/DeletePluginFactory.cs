using System.ComponentModel.Composition;
using System.IO;
using FolderWatcher.Services;
using FolderWatcher.Watcher;
using Newtonsoft.Json;

namespace FolderWatcher.Plugins.Delete
{
    [Export(typeof(IPluginFactory))]
    public class DeletePluginFactory : PluginFactoryBase<DeletePlugin, DeletePluginConfig>
    {
        private readonly IFileSystemService _fileSystemService;
        [ImportingConstructor]
        public DeletePluginFactory(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }

        protected override DeletePluginConfig CreateConfig(string configFile)
        {
            return new DeletePluginConfig(configFile);
        }

        protected override DeletePlugin CreatePlugin(DeletePluginConfig config)
        {
            return new DeletePlugin(config, _fileSystemService);
        }
    }
}