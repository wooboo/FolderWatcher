using System.ComponentModel.Composition;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Core.Plugins.Null
{
    [Export(typeof(IPluginFactory))]
    public class NullPluginFactory : PluginFactoryBase<NullPlugin, NullPluginConfig>
    {
        protected override NullPluginConfig CreateConfig(string configFile)
        {
            return new NullPluginConfig(configFile);
        }

        protected override NullPlugin CreatePlugin(NullPluginConfig config)
        {
            return new NullPlugin(config);
        }
    }
}