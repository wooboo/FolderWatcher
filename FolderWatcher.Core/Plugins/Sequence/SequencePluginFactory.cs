using System.ComponentModel.Composition;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Core.Plugins.Sequence
{
    [Export(typeof(IPluginFactory))]
    public class SequencePluginFactory : PluginFactoryBase<SequencePlugin, SequencePluginConfig>
    {
        protected override SequencePluginConfig CreateConfig(string configFile)
        {
            return new SequencePluginConfig(configFile);
        }

        protected override SequencePlugin CreatePlugin(SequencePluginConfig config)
        {
            return new SequencePlugin(config);
        }
    }
}