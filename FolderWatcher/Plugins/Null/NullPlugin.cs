using System;
using System.Threading.Tasks;
using FolderWatcher.Services;
using FolderWatcher.Services.Events;
using FolderWatcher.Watcher;

namespace FolderWatcher.Plugins.Null
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