using System.ComponentModel.Composition;
using FolderWatcher.Services;
using FolderWatcher.Watcher;

namespace FolderWatcher.Plugins.PluginSelector
{
    [Export(typeof(IPluginFactory))]
    public class SelectorPluginFactory : PluginFactoryBase<SelectorPlugin,SelectorPluginConfig>
    {
        [ImportingConstructor]
        public SelectorPluginFactory()
        {
        }

        protected override SelectorPluginConfig CreateConfig(string path)
        {
            return new SelectorPluginConfig(path);
        }

        protected override SelectorPlugin CreatePlugin(SelectorPluginConfig config)
        {
            return new SelectorPlugin(config);
        }
    }
}