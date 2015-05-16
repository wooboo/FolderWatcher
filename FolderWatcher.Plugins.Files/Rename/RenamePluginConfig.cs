using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.Files.Rename
{
    public class RenamePluginConfig : PluginConfigBase
    {
        public RenamePluginConfig(string path) : base(path)
        {
        }

        public string NewName { get; set; }
    }
}