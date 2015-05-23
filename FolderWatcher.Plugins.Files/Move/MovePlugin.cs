using System.IO;
using System.Linq;
using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using Microsoft.VisualBasic.FileIO;

namespace FolderWatcher.Plugins.Files.Move
{
    public class MovePlugin : PluginBase<MovePluginConfig>
    {

        public MovePlugin(MovePluginConfig config) : base(config)
        {
        }


        public override void OnFilesChange(FileSystemChangeSet fileSystemChangeSet)
        {
            if (fileSystemChangeSet.Added.Any())
            {
                var source = fileSystemChangeSet.Added.Select(o => o.FullPath);
                var dest = fileSystemChangeSet.Added.Select(o => Path.Combine(Config.Destination, o.Name));
                ShellLib.ShellFileOperation fo = new ShellLib.ShellFileOperation();

                fo.Operation = ShellLib.ShellFileOperation.FileOperations.FO_MOVE;
                fo.SourceFiles = source;
                fo.DestFiles = dest;

                bool RetVal = fo.DoOperation();
            }
        }
    }
}