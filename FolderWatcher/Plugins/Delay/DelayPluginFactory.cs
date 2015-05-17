using System.ComponentModel.Composition;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.Delay
{
    [Export(typeof (IPluginFactory))]
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