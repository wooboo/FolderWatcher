using System.Collections.Generic;
using FolderWatcher.Common.Model;

namespace FolderWatcher.Common.Events
{
    public class FileSystemChangeSet
    {
        public IEnumerable<FileChangeInfo> Added { get; set; }
        public IEnumerable<string> Deleted { get; set; }
        public string FolderPath { get; set; }
    }
}