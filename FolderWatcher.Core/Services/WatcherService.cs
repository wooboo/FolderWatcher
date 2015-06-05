using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Services;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FolderWatcher.Core.Services
{
    [Export(typeof (IWatcherService))]
    public class WatcherService : IWatcherService
    {
        private readonly Configuration _configuration;
        private readonly PluginManager _pluginManager;
        private readonly IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public WatcherService(Configuration configuration,
            PluginManager pluginManager,
            IEventAggregator eventAggregator)
        {
            _configuration = configuration;
            _pluginManager = pluginManager;
            _eventAggregator = eventAggregator;
            configuration.Load();
            Folders = new ObservableCollection<IFolder>();
            foreach (var folder in configuration.Folders)
            {
                var folderWatcher = new Folder(pluginManager, eventAggregator, folder);
                folderWatcher.Start();
                Folders.Add(folderWatcher);
            }
        }

        public ObservableCollection<IFolder> Folders { get; set; }
        public void AddFolder(string path)
        {
            var folder = new DirectorySettings()
            {
                Path = path
            };
            var folderWatcher = new Folder(_pluginManager, _eventAggregator, folder);
            folderWatcher.Start();
            Folders.Add(folderWatcher);
            _configuration.Folders.Add(folder);
            _configuration.Save();
        }

        public void RemoveFolder(string path)
        {
            var folder  =Folders.SingleOrDefault(o => o.FullPath == path) as Folder;
            if (folder != null)
            {
                folder.Stop();
                Folders.Remove(folder);
            }
        }
    }
}