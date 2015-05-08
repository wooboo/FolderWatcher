using System.Collections.Generic;

namespace FolderWatcher.Model
{
    public class Directory
    {
        public string Path { get; set; }
        public string Filter { get; set; }
        public IList<Plugin> Plugins { get; set; }
    }
}