using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;

namespace FolderWatcher.Common.Plugins
{
    public interface IPlugin
    {
        PluginMetadata Metadata { get; }
        void OnFilesChange(FileSystemChangeSet fileSystemChangeSet);
        //void OnFileCreated(FileChangeInfo file);
        //void OnFileDeleted(string file);
    }
}