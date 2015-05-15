using System.Collections.Generic;

namespace FolderWatcher.Services.Events
{
    public class FileSystemChangeSet
    {
        public IList<FileSystemItem> Added { get; set; }
        public IList<FileSystemItem> Deleted { get; set; }
        public IList<FileSystemItem> Changed { get; set; }

    }
}