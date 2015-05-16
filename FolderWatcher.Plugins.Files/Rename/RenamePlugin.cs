using System.IO;
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


        public override void OnFileCreated(FileChangeInfo file)
        {
            if (File.GetAttributes(file.FullPath).HasFlag(FileAttributes.Directory))
            {
                FileSystem.RenameDirectory(file.FullPath,Config.NewName);
            }
            else
            {
                FileSystem.RenameFile(file.FullPath, Config.NewName);
            }
        }

    }
}