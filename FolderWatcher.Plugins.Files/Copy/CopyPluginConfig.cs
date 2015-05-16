using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.Files.Copy
{
    public class CopyPluginConfig : PluginConfigBase
    {
        public CopyPluginConfig(string path) : base(path)
        {
        }

        public string Destination { get; set; }
    }
}