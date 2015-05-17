using FolderWatcher.Common.Model;

namespace FolderWatcher.Common.Plugins
{
    public interface IPlugin
    {
        PluginMetadata Metadata { get; }
        void OnFileCreated(FileChangeInfo file);
        void OnFileDeleted(FileChangeInfo file);
    }
}