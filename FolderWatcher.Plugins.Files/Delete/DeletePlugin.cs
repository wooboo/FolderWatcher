using System.IO;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using Microsoft.VisualBasic.FileIO;

namespace FolderWatcher.Plugins.Files.Delete
{
    public class DeletePlugin : PluginBase<DeletePluginConfig>
    {

        public DeletePlugin(DeletePluginConfig config) : base(config)
        {
        }


        public override void OnFileCreated(FileChangeInfo file)
        {
            if (File.GetAttributes(file.FullPath).HasFlag(FileAttributes.Directory))
            {
                FileSystem.DeleteDirectory(file.FullPath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            }
            else
            {
                FileSystem.DeleteFile(file.FullPath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            }
        }

    }
}