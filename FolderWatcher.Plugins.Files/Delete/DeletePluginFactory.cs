using System.ComponentModel.Composition;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.Files.Delete
{
    [Export(typeof(IPluginFactory))]
    public class DeletePluginFactory : PluginFactoryBase<DeletePlugin, DeletePluginConfig>
    {
        [ImportingConstructor]
        public DeletePluginFactory()
        {
        }

        protected override DeletePluginConfig CreateConfig(string configFile)
        {
            return new DeletePluginConfig(configFile);
        }

        protected override DeletePlugin CreatePlugin(DeletePluginConfig config)
        {
            return new DeletePlugin(config);
        }
    }
}