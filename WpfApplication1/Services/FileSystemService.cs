using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using FolderWatcher.Model;
using FolderWatcher.Watcher;

namespace FolderWatcher.Services
{
    [Export(typeof(IFileSystemService))]
    public class FileSystemService:IFileSystemService
    {
        [ImportingConstructor]
        public FileSystemService(Configuration configuration, [ImportMany] IPlugin[] plugins)
        {
            Watchers = new ObservableCollection<Folder>();
            foreach (var folder in configuration.Folders)
            {
                var folderWatcher = new Folder(this, plugins, folder);
                folderWatcher.Start();
                Watchers.Add(folderWatcher);
            }
        }
        public ObservableCollection<Folder> Watchers { get; set; }
        public FileAction ForFile(ChangedFile file)
        {
            return new FileAction(this,file);
        }
    }
}
