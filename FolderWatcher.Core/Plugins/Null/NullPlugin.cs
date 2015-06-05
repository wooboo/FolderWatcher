using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Common.Services;

namespace FolderWatcher.Core.Plugins.Null
{
    public class NullPlugin : PluginBase<NullPluginConfig>
    {

        public NullPlugin(NullPluginConfig config) : base(config)
        {
        }


        public override void OnFileCreated(FileChangeInfo file, IValueBag valueBag)
        {
        }

    }
}