using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Core.Plugins.Null
{
    public class NullPluginConfig : PluginConfigBase
    {
        public NullPluginConfig(string path) : base(path)
        {
        }

        public override void Save()
        {
        }

        public override bool Load()
        {
            return true;
        }
    }
}