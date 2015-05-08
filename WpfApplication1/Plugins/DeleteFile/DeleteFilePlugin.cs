using System.ComponentModel.Composition;
using FolderWatcher.Services;
using FolderWatcher.Watcher;

namespace FolderWatcher.Plugins.DeleteFile
{
    [Export(typeof(IPlugin))]
    public class DeleteFilePlugin:IPlugin
    {
        public string Name { get { return GetType().Name; } }

        public void Init(dynamic settings)
        {
        }

        public void OnFile(IFileSystemService fileSystemService, ChangedFile file)
        {
            file.PluginParts.Add(new DeleteFilePluginPart(fileSystemService, file));
        }

    }
}