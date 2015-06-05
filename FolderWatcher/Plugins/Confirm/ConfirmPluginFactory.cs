using System.ComponentModel.Composition;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.Confirm
{
    [Export(typeof (IPluginFactory))]
    public class ConfirmPluginFactory : PluginFactoryBase<ConfirmPlugin, ConfirmPluginConfig>
    {
        [ImportingConstructor]
        public ConfirmPluginFactory()
        {
        }

        protected override ConfirmPluginConfig CreateConfig(string configFile)
        {
            return new ConfirmPluginConfig(configFile);
        }

        protected override ConfirmPlugin CreatePlugin(ConfirmPluginConfig config)
        {
            return new ConfirmPlugin(config);
        }
    }
}