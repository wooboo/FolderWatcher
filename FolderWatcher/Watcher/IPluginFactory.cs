using System.Collections.Generic;

namespace FolderWatcher.Watcher
{
    public interface IPluginFactory
    {
        string Name { get; }
        string PluginNamePattern { get; }
        IEnumerable<IPlugin> LoadPlugins(string path, IEnumerable<string> pluginNames = null);
        IEnumerable<string> FilterConfigs(IEnumerable<string> plugins);
        //IEnumerable<IPlugin> CreatePlugins(string path);
    }
}