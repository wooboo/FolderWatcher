using System.IO;

namespace FolderWatcher.Services.Events
{
    public class FileSystemItem
    {

        public FileSystemItem()
        {
            
        }

        public FileSystemItem(string path)
        {
            FullPath = path;
            Name = Path.GetFileName(FullPath);
        }
        public string Name { get; set; }
        public string FullPath { get; set; }
    }
}