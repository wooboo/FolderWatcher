using System.ComponentModel.Composition;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.Files.Rename
{
    [Export(typeof(IPluginFactory))]
    public class RenamePluginFactory : PluginFactoryBase<RenamePlugin, RenamePluginConfig>
    {
        [ImportingConstructor]
        public RenamePluginFactory()
        {
        }

        protected override RenamePluginConfig CreateConfig(string configFile)
        {
            return new RenamePluginConfig(configFile);
        }

        protected override RenamePlugin CreatePlugin(RenamePluginConfig config)
        {
            return new RenamePlugin(config);
        }
    }
}