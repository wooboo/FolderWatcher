using System.Collections.Generic;

namespace FolderWatcher.Watcher
{
    public interface IPluginFactory
    {
        string Name { get; }
        IEnumerable<IPlugin> LoadPlugins(string path);
        IEnumerable<IPlugin> CreatePlugins(string path);
    }
}