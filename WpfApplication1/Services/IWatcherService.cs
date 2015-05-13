using System.Collections.ObjectModel;
using FolderWatcher.Watcher;

namespace FolderWatcher.Services
{
    public interface IWatcherService
    {
        ObservableCollection<Folder> Watchers { get; }
    }
}