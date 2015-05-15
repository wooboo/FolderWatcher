using System.ComponentModel.Composition;
using System.IO;
using FolderWatcher.Services;
using FolderWatcher.Watcher;
using Newtonsoft.Json;

namespace FolderWatcher.Plugins.Delay
{
    [Export(typeof(IPluginFactory))]
    public class DelayPluginFactory : PluginFactoryBase<DelayPlugin, DelayPluginConfig>
    {
        private readonly IFileSystemService _fileSystemService;
        [ImportingConstructor]
        public DelayPluginFactory(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }

        protected override DelayPluginConfig CreateConfig(string path)
        {
            return new DelayPluginConfig(path);
        }

        protected override DelayPlugin CreatePlugin(DelayPluginConfig config)
        {
            return new DelayPlugin(config, _fileSystemService);
        }
    }
}