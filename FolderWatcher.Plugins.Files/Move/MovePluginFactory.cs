using System.ComponentModel.Composition;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.Files.Move
{
    [Export(typeof(IPluginFactory))]
    public class MovePluginFactory : PluginFactoryBase<MovePlugin, MovePluginConfig>
    {
        [ImportingConstructor]
        public MovePluginFactory()
        {
        }

        protected override MovePluginConfig CreateConfig(string configFile)
        {
            return new MovePluginConfig(configFile);
        }

        protected override MovePlugin CreatePlugin(MovePluginConfig config)
        {
            return new MovePlugin(config);
        }
    }
}