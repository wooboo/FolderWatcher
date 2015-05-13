using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using FolderWatcher.Services;
using FolderWatcher.Watcher;
using Microsoft.Practices.Prism.Mvvm;

namespace FolderWatcher.Shell
{
    [Export]
    public class ShellViewModel:BindableBase
    {
        private readonly IWatcherService _watcherService;
        private ObservableCollection<Folder> _watchers;

        [ImportingConstructor]
        public ShellViewModel(IWatcherService watcherService)
        {
            _watcherService = watcherService;
            this.Watchers = _watcherService.Watchers;
            //this.Watchers = new ObservableCollection<Folder>();
            //this.Watchers.Add(new Folder("~\\Downloads","*.*")
            //{
            //    Files = new ObservableCollection<ChangedFile>
            //    {
            //        new ChangedFile("~\\Downloads\\aaa.txt"),
            //        new ChangedFile("~\\Downloads\\aaa.txt"),
            //        new ChangedFile("~\\Downloads\\aaa.txt"),
            //        new ChangedFile("~\\Downloads\\aaa.txt"),
            //        new ChangedFile("~\\Downloads\\aaa.txt"),
            //        new ChangedFile("~\\Downloads\\aaa.txt"),
            //        new ChangedFile("~\\Downloads\\aaa.txt"),
            //        new ChangedFile("~\\Downloads\\aaa.txt"),
            //        new ChangedFile("~\\Downloads\\aaa.txt"),
            //    }
            //});
        }

        public ObservableCollection<Folder> Watchers
        {
            get { return _watchers; }
            set { SetProperty(ref _watchers, value); }
        }

    }
}
