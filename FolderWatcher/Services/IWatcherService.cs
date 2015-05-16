using System.Collections.ObjectModel;
using FolderWatcher.Core.Services;

namespace FolderWatcher.Services
{
    public interface IWatcherService
    {
        ObservableCollection<Folder> Folders { get; }
    }
}