using System.ComponentModel.Composition;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Core.Plugins.Selector
{
    [Export(typeof(IPluginFactory))]
    public class SelectorPluginFactory : PluginFactoryBase<SelectorPlugin,SelectorPluginConfig>
    {
        [ImportingConstructor]
        public SelectorPluginFactory()
        {
        }

        protected override SelectorPluginConfig CreateConfig(string configFile)
        {
            return new SelectorPluginConfig(configFile);
        }

        protected override SelectorPlugin CreatePlugin(SelectorPluginConfig config)
        {
            return new SelectorPlugin(config);
        }
    }
}