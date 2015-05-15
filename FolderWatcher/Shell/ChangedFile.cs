using System.Collections.Generic;
using System.IO;

namespace FolderWatcher.Watcher
{
    public class ChangedFile
    {
        public ChangedFile(string path)
        {
            FullPath = path;
            Name = Path.GetFileName(FullPath);
            PluginParts = new List<IPluginPart>();
        }

        public string FullPath { get; private set; }
        public string Name { get; set; }
        public List<IPluginPart> PluginParts { get; set; }
    }
}