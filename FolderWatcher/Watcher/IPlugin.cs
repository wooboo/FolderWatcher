using FolderWatcher.Services;

namespace FolderWatcher.Watcher
{
    public interface IPlugin
    {
        void OnFile(ChangedFile file);
    }
}