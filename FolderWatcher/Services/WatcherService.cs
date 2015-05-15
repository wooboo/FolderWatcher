using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Threading;
using FolderWatcher.Model;
using FolderWatcher.Watcher;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FolderWatcher.Services
{
    [Export(typeof(IWatcherService))]
    public class WatcherService : IWatcherService
    {

        private readonly IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public WatcherService(Configuration configuration, 
            [ImportMany] IPluginFactory[] pluginFactories, 
            IFileSystemService fileSystemService, 
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Folders = new ObservableCollection<Folder>();
            foreach (var folder in configuration.Folders)
            {
                var folderWatcher = new Folder(fileSystemService, pluginFactories, eventAggregator, folder);
                folderWatcher.Start();
                Folders.Add(folderWatcher);
            }


        }

        


        public ObservableCollection<Folder> Folders { get; set; }
 
    }
}