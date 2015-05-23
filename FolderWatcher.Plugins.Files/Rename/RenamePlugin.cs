using System.IO;
using System.Linq;
using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using Microsoft.VisualBasic.FileIO;

namespace FolderWatcher.Plugins.Files.Rename
{
    public class RenamePlugin : PluginBase<RenamePluginConfig>
    {

        public RenamePlugin(RenamePluginConfig config) : base(config)
        {
        }

        public override void OnFilesChange(FileSystemChangeSet fileSystemChangeSet)
        {
            if (fileSystemChangeSet.Added.Any())
            {
                var source = fileSystemChangeSet.Added.Select(o => o.FullPath);
                var dest = fileSystemChangeSet.Added.Select(o => Config.NewName);
                ShellLib.ShellFileOperation fo = new ShellLib.ShellFileOperation();

                fo.Operation = ShellLib.ShellFileOperation.FileOperations.FO_COPY;
                fo.SourceFiles = source;
                fo.DestFiles = dest;

                bool RetVal = fo.DoOperation();
            }
        }

    }
}