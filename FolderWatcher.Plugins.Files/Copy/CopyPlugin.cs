using System.IO;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using Microsoft.VisualBasic.FileIO;

namespace FolderWatcher.Plugins.Files.Copy
{
    public class CopyPlugin : PluginBase<CopyPluginConfig>
    {

        public CopyPlugin(CopyPluginConfig config) : base(config)
        {
        }


        public override void OnFileCreated(FileChangeInfo file)
        {
            //TODO: Grouping and Throttling (here and other file plugins)
            if (File.GetAttributes(file.FullPath).HasFlag(FileAttributes.Directory))
            {
                //TODO: test and copy directory not only content
                FileSystem.CopyDirectory(file.FullPath, Path.Combine(Config.Destination, file.Name), UIOption.AllDialogs);
            }
            else
            {
                FileSystem.CopyFile(file.FullPath, Path.Combine(Config.Destination, file.Name), UIOption.AllDialogs);
            }
        }

    }
}