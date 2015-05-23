using System.ComponentModel.Composition;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.Files.Extract
{
    [Export(typeof(IPluginFactory))]
    public class ExtractPluginFactory : PluginFactoryBase<ExtractPlugin, ExtractPluginConfig>
    {
        [ImportingConstructor]
        public ExtractPluginFactory()
        {
        }

        protected override ExtractPluginConfig CreateConfig(string configFile)
        {
            return new ExtractPluginConfig(configFile);
        }

        protected override ExtractPlugin CreatePlugin(ExtractPluginConfig config)
        {
            return new ExtractPlugin(config);
        }
    }
}