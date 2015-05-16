using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Core.Plugins.Null
{
    public class NullPlugin : PluginBase<NullPluginConfig>
    {

        public NullPlugin(NullPluginConfig config) : base(config)
        {
        }


        public override void OnFileCreated(FileChangeInfo file)
        {
        }

    }
}