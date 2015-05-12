using System;

namespace FolderWatcher.Services
{
    public class DelayedFileDeletion
    {
        public string Path { get; set; }
        public DateTime OrderDate { get; set; }
        public TimeSpan Delay { get; set; }
    }
}