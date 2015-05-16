using System.IO;
using FolderWatcher.Services;
using FolderWatcher.Services.Events;

namespace FolderWatcher.Watcher
{
    public interface IPlugin
    { 
        PluginName Name{get;}
        void OnFileCreated(FileChangeInfo file);
        void OnFileDeleted(FileChangeInfo file);
    }

    public class PluginName
    {
        public PluginName(string fullName)
        {
            FullName = fullName;
            Name = fullName.Split('.')[0];
            InstanceName = fullName.Split('.')[1];
        }
        public string Name { get; set; }
        public string InstanceName { get; set; }
        public string FullName { get; set; }
    }
}