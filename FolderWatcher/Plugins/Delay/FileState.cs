using System;
using FolderWatcher.Services.Events;

namespace FolderWatcher.Plugins.Delay
{
    public class FileState
    {
        public FileChangeInfo File { get; set; }
        public DateTime CreateDate { get; set; }
        public TimeSpan DelayAfter { get; set; }
    }
}