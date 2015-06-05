using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Services;

namespace FolderWatcher.Common.Plugins
{
    public interface IPlugin
    {
        PluginMetadata Metadata { get; }
        void OnFilesChange(FileSystemChangeSet fileSystemChangeSet, IValueBag valueBag);
        //void OnFileCreated(FileChangeInfo file);
        //void OnFileDeleted(string file);
    }
}