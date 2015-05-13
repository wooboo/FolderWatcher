namespace FolderWatcher.Watcher
{
    public interface IPluginFactory
    {
        string Name { get; }
        bool TryCreatePlugin(string path, out IPlugin plugin);
    }
}