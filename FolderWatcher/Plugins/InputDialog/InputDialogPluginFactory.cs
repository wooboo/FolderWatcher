using System.ComponentModel.Composition;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.InputDialog
{
    [Export(typeof (IPluginFactory))]
    public class InputDialogPluginFactory : PluginFactoryBase<InputDialogPlugin, InputDialogPluginConfig>
    {
        [ImportingConstructor]
        public InputDialogPluginFactory()
        {
        }

        protected override InputDialogPluginConfig CreateConfig(string configFile)
        {
            return new InputDialogPluginConfig(configFile);
        }

        protected override InputDialogPlugin CreatePlugin(InputDialogPluginConfig config)
        {
            return new InputDialogPlugin(config);
        }
    }
}