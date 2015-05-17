using System.IO;
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


        public override void OnFileCreated(FileChangeInfo file)
        {
            if (File.GetAttributes(file.FullPath).HasFlag(FileAttributes.Directory))
            {
                FileSystem.MoveDirectory(file.FullPath, Path.Combine(Config.Destination, file.Name),UIOption.AllDialogs);
            }
            else
            {
                FileSystem.MoveFile(file.FullPath, Path.Combine(Config.Destination, file.Name), UIOption.AllDialogs);
            }
        }

    }
}