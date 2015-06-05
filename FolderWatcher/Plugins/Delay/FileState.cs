using System;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Services;

namespace FolderWatcher.Plugins.Delay
{
    public class FileState
    {
        public FileChangeInfo File { get; set; }
        public DateTime CreateDate { get; set; }
        public TimeSpan DelayAfter { get; set; }
        public IValueBag ValueBag { get; set; }
    }
}