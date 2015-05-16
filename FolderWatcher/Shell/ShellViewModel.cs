using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Input;
using FolderWatcher.Plugins.Buttons;
using FolderWatcher.Services;
using FolderWatcher.Services.Events;
using FolderWatcher.Watcher;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Shell
{
    [Export]
    public class ShellViewModel : BindableBase
    {
        private readonly IWatcherService _watcherService;
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<FolderViewModel> _folders;
        public ICommand Setup { get; set; }

        private void SetupDirectory()
        {
            var path = Path.Combine(_folders[0].FullPath, ".watcher");
            var factories = ServiceLocator.Current.GetAllInstances<IPluginFactory>();

        }
        [ImportingConstructor]
        public ShellViewModel(IWatcherService watcherService, IEventAggregator eventAggregator)
        {
            _watcherService = watcherService;
            _eventAggregator = eventAggregator;
            Setup = new DelegateCommand(SetupDirectory);
            _eventAggregator.GetEvent<FilesEvent>().Subscribe(o =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    var folder = Folders.Single(f => f.FullPath == o.FolderPath);
                    foreach (var fileChangeInfo in o.Added)
                    {
                        folder.Files.Add(new FileViewModel(fileChangeInfo.FullPath));
                    }
                    foreach (var path in o.Deleted)
                    {
                        var item = folder.Files.Single(f => f.FullPath == path);
                        folder.Files.Remove(item);
                    }
                });
            });
            _eventAggregator.GetEvent<AddPluginPartEvent>().Subscribe(part =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    var folder = Folders.Single(f => f.FullPath == part.FolderPath);
                    folder.Files.Single(o => o.FullPath == part.FilePath).PluginParts.Add(part.Part);
                });
            });
            this.Folders =
                new ObservableCollection<FolderViewModel>(
                    _watcherService.Folders.Select(o => new FolderViewModel()
                    {
                        Name = o.Name, FullPath = o.FullPath
                    
                    }));

        }

        public ObservableCollection<FolderViewModel> Folders
        {
            get { return _folders; }
            set { SetProperty(ref _folders, value); }
        }

    }
    public class FolderViewModel : BindableBase
    {
        private ObservableCollection<FileViewModel> _files = new ObservableCollection<FileViewModel>();
        public string Name { get; set; }
        public string FullPath { get; set; }

        public ObservableCollection<FileViewModel> Files
        {
            get { return _files; }
            set { SetProperty(ref _files, value); }
        }
    }
}
