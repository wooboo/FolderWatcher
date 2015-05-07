using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Model;
using WpfApplication1.Watcher;

namespace WpfApplication1.Services
{
    [Export(typeof(ISomeService))]
    public class SomeService:ISomeService
    {
        [ImportingConstructor]
        public SomeService(Configuration configuration)
        {
            Watchers = new ObservableCollection<FolderWatcher>();
            foreach (var folder in configuration.Folders)
            {
                var folderWatcher = new FolderWatcher(folder);
                folderWatcher.Start();
                Watchers.Add(folderWatcher);
            }
        }
        public ObservableCollection<FolderWatcher> Watchers { get; set; }
    }

    public interface ISomeService
    {
        ObservableCollection<FolderWatcher> Watchers { get; } 
    }
}
