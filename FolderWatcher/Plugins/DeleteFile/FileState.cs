using System;

namespace FolderWatcher.Plugins.DeleteFile
{
    public class FileState
    {
        public string Path { get; set; }
        public DateTime CreateDate { get; set; }
        public TimeSpan DeleteAfter { get; set; }
    }
}