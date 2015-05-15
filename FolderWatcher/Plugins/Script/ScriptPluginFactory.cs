using System.ComponentModel.Composition;
using FolderWatcher.Watcher;

namespace FolderWatcher.Plugins.Script
{
    [Export(typeof(IPluginFactory))]
    public class ScriptPluginFactory : PluginFactoryBase<ScriptPlugin,ScriptPluginConfig>
    {
        [ImportingConstructor]
        public ScriptPluginFactory()
        {
        }

        protected override ScriptPluginConfig CreateConfig(string path)
        {
            return new ScriptPluginConfig(path);
        }

        protected override ScriptPlugin CreatePlugin(ScriptPluginConfig config)
        {
            return new ScriptPlugin(config);
        }
    }
}