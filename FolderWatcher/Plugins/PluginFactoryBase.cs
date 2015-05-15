using System.Collections.Generic;
using System.IO;
using FolderWatcher.Plugins.DeleteFile;
using FolderWatcher.Watcher;

namespace FolderWatcher.Plugins
{
    public abstract class PluginFactoryBase<TPlugin, TConfig> : IPluginFactory 
        where TPlugin: IPlugin 
        where TConfig : PluginConfigBase
    {
        public string Name => typeof (TPlugin).Name;
        public IEnumerable<IPlugin> LoadPlugins(string path)
        {
            var configFiles = Directory.GetFiles(path, Name + ".*.json");
            foreach (var configFile in configFiles)
            {
                foreach (var plugin in CreatePlugins(configFile))
                {
                    yield return plugin;
                }
            }
        }

        public IEnumerable<IPlugin> CreatePlugins(string configFile)
        {

            var config = CreateConfig(configFile);
            if (config.TryLoad())
            {
                yield return CreatePlugin(config);
            }
        }

        protected abstract TConfig CreateConfig(string path);

        protected abstract TPlugin CreatePlugin(TConfig config);
    }
}