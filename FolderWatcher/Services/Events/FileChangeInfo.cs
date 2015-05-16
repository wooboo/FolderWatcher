using System;
using System.IO;

namespace FolderWatcher.Services.Events
{
    public class FileChangeInfo
    {
        public FileChangeInfo(string fullPath)
        {
            Name = Path.GetFileName(fullPath);
            FullPath = fullPath;
            LastWriteTime = File.GetLastWriteTime(fullPath);
            Attributes = File.GetAttributes(fullPath);
        }

        public FileChangeInfo()
        {

        }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public DateTime LastWriteTime { get; set; }
        public FileAttributes Attributes { get; set; }

        public bool HasChanged()
        {
            var newWrite = File.GetLastWriteTime(FullPath);
            var newAttrs = File.GetAttributes(FullPath);
            if (newWrite != LastWriteTime || newAttrs != Attributes)
            {
                LastWriteTime = newWrite;
                Attributes = newAttrs;
                return true;
            }
            return false;
        }
    }
}