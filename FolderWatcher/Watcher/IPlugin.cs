using FolderWatcher.Services;
using FolderWatcher.Services.Events;

namespace FolderWatcher.Watcher
{
    public interface IPlugin
    {
        void OnFile(FileSystemItem file);
    }
}