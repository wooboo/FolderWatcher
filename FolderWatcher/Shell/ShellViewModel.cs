using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using Ookii.Dialogs.Wpf;

namespace FolderWatcher.Shell
{
    [Export]
    public class ShellViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWatcherService _watcherService;
        private ObservableCollection<FolderViewModel> _folders;
        private FolderViewModel _selectedFolder;

        [ImportingConstructor]
        public ShellViewModel(IWatcherService watcherService, IEventAggregator eventAggregator)
        {
            _watcherService = watcherService;
            _eventAggregator = eventAggregator;
            Add = new DelegateCommand(AddDirectory);
            Remove = new DelegateCommand(RemoveDirectory, CanRemoveDirectory);
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
            _watcherService.Folders.CollectionChanged += Folders_CollectionChanged;
        }

        private void Folders_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var o in e.NewItems.OfType<IFolder>())
                {
                    Folders.Add(new FolderViewModel
                    {
                        Name = o.Name,
                        FullPath = o.FullPath
                    });
                }
            }else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldItem in e.OldItems.OfType<IFolder>())
                {
                    var folder = Folders.FirstOrDefault(o => o.FullPath == oldItem.FullPath);
                    if (folder != null)
                    {
                        Folders.Remove(folder);
                    }
                }
            }
        }

        public ICommand Add { get; set; }
        public ICommand Remove { get; set; }

        public ObservableCollection<FolderViewModel> Folders
        {
            get { return _folders; }
            set { SetProperty(ref _folders, value); }
        }

        public FolderViewModel SelectedFolder
        {
            get { return _selectedFolder; }
            set { SetProperty(ref _selectedFolder, value); }
        }

        private bool CanRemoveDirectory()
        {
            return SelectedFolder != null;
        }
        private void RemoveDirectory()
        {
            
        }
        private void AddDirectory()
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() ?? true)
            {
                _watcherService.AddFolder(dialog.SelectedPath);
            }
        }
    }
}