using System.Collections.Generic;

namespace FolderWatcher.Plugins.DeleteFile
{
    public class DeleteFilePluginConfig : PluginConfigBase
    {
        public DeleteFilePluginConfig(string path) : base(path)
        {
        }

        public IList<FileState> FileStates { get; set; } = new List<FileState>();
    }
}