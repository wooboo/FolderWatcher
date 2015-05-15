using System.Collections.Generic;

namespace FolderWatcher.Services.Events
{
    public class FileSystemChangeSet
    {
        public IEnumerable<FileChangeInfo> Added { get; set; }
        public IEnumerable<string> Deleted { get; set; }
        public string FolderPath { get; set; }
    }
}