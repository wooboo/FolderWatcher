using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.Files.Move
{
    public class MovePluginConfig : PluginConfigBase
    {
        public MovePluginConfig(string path) : base(path)
        {
        }

        public string Destination { get; set; }
    }
}