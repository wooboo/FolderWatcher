using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Threading;
using FolderWatcher.Model;
using FolderWatcher.Watcher;

namespace FolderWatcher.Services
{
    [Export(typeof(IFileSystemService))]
    public class FileSystemService:IFileSystemService
    {
        private readonly IDelayedActionPlugin[] _sweepers;
        readonly DispatcherTimer _dispatcherTimer;

        [ImportingConstructor]
        public FileSystemService(Configuration configuration, [ImportMany] IPlugin[] plugins, [ImportMany] IDelayedActionPlugin[] sweepers)
        {
            _sweepers = sweepers;
            Watchers = new ObservableCollection<Folder>();
            foreach (var folder in configuration.Folders)
            {
                var folderWatcher = new Folder(this, plugins, folder);
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
            foreach (var delayedActionPlugin in _sweepers)
            {
                delayedActionPlugin.Sweep();
            }
        }

        public ObservableCollection<Folder> Watchers { get; set; }
        public FileAction ForFile(ChangedFile file)
        {
            return new FileAction(this,file);
        }
    }
}
