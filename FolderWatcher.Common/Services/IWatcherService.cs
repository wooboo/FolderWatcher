using System.Collections.ObjectModel;

namespace FolderWatcher.Common.Services
{
    public interface IWatcherService
    {
        ObservableCollection<IFolder> Folders { get; }
    }
}