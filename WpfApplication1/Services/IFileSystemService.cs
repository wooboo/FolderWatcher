using System.Collections.ObjectModel;
using FolderWatcher.Watcher;

namespace FolderWatcher.Services
{
    public interface IFileSystemService
    {
        ObservableCollection<Folder> Watchers { get; }
        FileAction ForFile(ChangedFile file);
    }
}