using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using FolderWatcher.Core.Services;
using FolderWatcher.Model;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FolderWatcher.Services
{
    [Export(typeof (IWatcherService))]
    public class WatcherService : IWatcherService
    {
        private readonly IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public WatcherService(Configuration configuration,
            PluginManager pluginManager,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Folders = new ObservableCollection<Folder>();
            foreach (var folder in configuration.Folders)
            {
                var folderWatcher = new Folder(pluginManager, eventAggregator, folder);
                folderWatcher.Start();
                Folders.Add(folderWatcher);
            }
        }

        public ObservableCollection<Folder> Folders { get; set; }
    }
}