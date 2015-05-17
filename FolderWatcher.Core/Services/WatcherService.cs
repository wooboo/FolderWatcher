using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Services;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FolderWatcher.Core.Services
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

            Folders = new ObservableCollection<IFolder>();
            foreach (var folder in configuration.Folders)
            {
                var folderWatcher = new Folder(pluginManager, eventAggregator, folder);
                folderWatcher.Start();
                Folders.Add(folderWatcher);
            }
        }

        public ObservableCollection<IFolder> Folders { get; set; }
    }
}