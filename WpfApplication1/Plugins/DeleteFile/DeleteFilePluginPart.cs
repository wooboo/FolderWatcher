using System;
using System.Windows.Input;
using FolderWatcher.Services;
using FolderWatcher.Watcher;
using Microsoft.Practices.Prism.Commands;

namespace FolderWatcher.Plugins.DeleteFile
{
    public class DeleteFilePluginPart : IPluginPart {
        private readonly DeleteFilePlugin _plugin;
        public ChangedFile File { get; private set; }

        public DeleteFilePluginPart(DeleteFilePlugin plugin, ChangedFile file)
        {
            _plugin = plugin;
            File = file;
            Delete = new DelegateCommand<ChangedFile>(DeleteFile);
            DelayedDelete = new DelegateCommand<ChangedFile>(DelayedDeleteFile);
        }

        private void DelayedDeleteFile(ChangedFile changedFile)
        {
             _plugin.DelayedDelete(changedFile, TimeSpan.FromMinutes(3));
            
        }

        private async void DeleteFile(ChangedFile changedFile)
        {
            await _plugin.Delete(changedFile);
        }

        public ICommand Delete { get; set; }
        public ICommand DelayedDelete { get; set; }
    }
}