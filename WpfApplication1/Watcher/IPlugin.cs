using FolderWatcher.Services;

namespace FolderWatcher.Watcher
{
    public interface IPlugin
    {
        string Name { get; }
        void Init(object settings);
        void OnFile(IFileSystemService fileSystemService,ChangedFile file);
    }
}