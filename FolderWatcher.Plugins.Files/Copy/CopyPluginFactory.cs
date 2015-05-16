using System.ComponentModel.Composition;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.Files.Copy
{
    [Export(typeof(IPluginFactory))]
    public class CopyPluginFactory : PluginFactoryBase<CopyPlugin, CopyPluginConfig>
    {
        [ImportingConstructor]
        public CopyPluginFactory()
        {
        }

        protected override CopyPluginConfig CreateConfig(string configFile)
        {
            return new CopyPluginConfig(configFile);
        }

        protected override CopyPlugin CreatePlugin(CopyPluginConfig config)
        {
            return new CopyPlugin(config);
        }
    }
}