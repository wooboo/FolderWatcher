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
        [ImportingConstructor]
        public DelayPluginFactory()
        {
        }

        protected override DelayPluginConfig CreateConfig(string configFile)
        {
            return new DelayPluginConfig(configFile);
        }

        protected override DelayPlugin CreatePlugin(DelayPluginConfig config)
        {
            return new DelayPlugin(config);
        }
    }
}