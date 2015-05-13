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
        readonly DispatcherTimer _dispatcherTimer;

        [ImportingConstructor]
        public WatcherService(Configuration configuration, [ImportMany] IPluginFactory[] pluginFactories, IFileSystemService fileSystemService, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Watchers = new ObservableCollection<Folder>();
            foreach (var folder in configuration.Folders)
            {
                var folderWatcher = new Folder(fileSystemService, pluginFactories, folder);
                folderWatcher.Start();
                Watchers.Add(folderWatcher);
            }
            _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            _dispatcherTimer.Tick += dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            _dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            foreach (var watcher in Watchers)
            {
                watcher.Sweep();
            }
        }


        public ObservableCollection<Folder> Watchers { get; set; }
 
    }
}