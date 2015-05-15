using System.ComponentModel.Composition;
using System.IO;
using FolderWatcher.Services;
using FolderWatcher.Watcher;
using Newtonsoft.Json;

namespace FolderWatcher.Plugins.Buttons
{
    [Export(typeof(IPluginFactory))]
    public class ButtonsPluginFactory : PluginFactoryBase<ButtonsPlugin, ButtonsPluginConfig>
    {
        private readonly IFileSystemService _fileSystemService;
        [ImportingConstructor]
        public ButtonsPluginFactory(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }

        protected override ButtonsPluginConfig CreateConfig(string path)
        {
            return new ButtonsPluginConfig(path);
        }

        protected override ButtonsPlugin CreatePlugin(ButtonsPluginConfig config)
        {
            return new ButtonsPlugin(config, _fileSystemService);
        }
    }
}