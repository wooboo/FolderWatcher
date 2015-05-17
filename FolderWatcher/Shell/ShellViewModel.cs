using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FolderWatcher.Common.Events;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Common.Services;
using FolderWatcher.Model;
using FolderWatcher.Plugins.Buttons;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Shell
{
    [Export]
    public class ShellViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWatcherService _watcherService;
        private ObservableCollection<FolderViewModel> _folders;

        [ImportingConstructor]
        public ShellViewModel(IWatcherService watcherService, IEventAggregator eventAggregator)
        {
            _watcherService = watcherService;
            _eventAggregator = eventAggregator;
            Setup = new DelegateCommand(SetupDirectory);
            _eventAggregator.GetEvent<FilesEvent>().Subscribe(o =>
            {
                Application.Current.Dispatcher.Invoke(() =>
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
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var folder = Folders.Single(f => f.FullPath == part.FolderPath);
                    folder.Files.Single(o => o.FullPath == part.FilePath).PluginParts.Add(part.Part);
                });
            });
            Folders =
                new ObservableCollection<FolderViewModel>(
                    _watcherService.Folders.Select(o => new FolderViewModel
                    {
                        Name = o.Name,
                        FullPath = o.FullPath
                    }));
        }

        public ICommand Setup { get; set; }

        public ObservableCollection<FolderViewModel> Folders
        {
            get { return _folders; }
            set { SetProperty(ref _folders, value); }
        }

        private void SetupDirectory()
        {
            var path = Path.Combine(_folders[0].FullPath, ".watcher");
            var factories = ServiceLocator.Current.GetAllInstances<IPluginFactory>();
        }
    }
}