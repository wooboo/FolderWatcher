using System.Collections.ObjectModel;

namespace FolderWatcher.Common.Services
{
    public interface IWatcherService
    {
        ObservableCollection<IFolder> Folders { get; }
        void AddFolder(string path);
        void RemoveFolder(string path);
    }
}