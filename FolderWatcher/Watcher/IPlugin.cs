using FolderWatcher.Services;
using FolderWatcher.Services.Events;

namespace FolderWatcher.Watcher
{
    public interface IPlugin
    {
        void OnFileCreated(FileChangeInfo file);
        void OnFileDeleted(FileChangeInfo file);
    }
}