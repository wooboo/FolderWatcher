using System.IO;
using System.Linq;
using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Common.Services;
using Microsoft.VisualBasic.FileIO;

namespace FolderWatcher.Plugins.Files.Delete
{
    public class DeletePlugin : PluginBase<DeletePluginConfig>
    {

        public DeletePlugin(DeletePluginConfig config) : base(config)
        {
        }

        public override void OnFilesChange(FileSystemChangeSet fileSystemChangeSet, IValueBag valueBag)
        {
            if (fileSystemChangeSet.Added.Any())
            {
                var source = fileSystemChangeSet.Added.Select(o => o.FullPath);
                ShellLib.ShellFileOperation fo = new ShellLib.ShellFileOperation();

                fo.Operation = ShellLib.ShellFileOperation.FileOperations.FO_DELETE;
                fo.SourceFiles = source;

                bool RetVal = fo.DoOperation();
            }
        }

    }
}