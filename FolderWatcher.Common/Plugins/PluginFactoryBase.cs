using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FolderWatcher.Common.Plugins
{
    public abstract class PluginFactoryBase<TPlugin, TConfig> : IPluginFactory 
        where TPlugin: IPlugin 
        where TConfig : PluginConfigBase
    {
        public string Name
        {
            get
            {
                var name = typeof (TPlugin).Name;
                if (name.EndsWith("Plugin"))
                {
                    name = name.Replace("Plugin", "");
                }
                return name;
            }
        }

        public IEnumerable<string> FilterConfigs(IEnumerable<string> plugins)
        {
            return plugins?.Where(o => FitsMask(o, PluginNamePattern));
        }
        private bool FitsMask(string sFileName, string sFileMask)
        {
            Regex mask = new Regex(sFileMask.Replace(".", "[.]").Replace("*", ".*").Replace("?", "."));
            return mask.IsMatch(sFileName);
        }

        public string PluginNamePattern => Name + ".*";

        public IEnumerable<IPlugin> LoadPlugins(string path, IEnumerable<string> pluginNames = null)
        {
            if (pluginNames == null)
            {
                pluginNames = Directory.GetFiles(path, PluginNamePattern+".json").Select(Path.GetFileNameWithoutExtension);
            }
            var configFiles = pluginNames.Select(o => Path.Combine(path, o)+".json");
            
            foreach (var config in GetConfigs(configFiles))
            {
                yield return CreatePlugin(config);
            }
            
        }

        //public IEnumerable<IPlugin> CreatePlugins(string configFile)
        //{

        //    var config = CreateConfig(configFile);
        //    if (config.Load())
        //    {
        //        yield return CreatePlugin(config);
        //    }
        //}

        public IEnumerable<TConfig> GetConfigs(IEnumerable<string> configFiles)
        {
            foreach (var configFile in configFiles)
            {
                var config = CreateConfig(configFile);
                config.Load();
                yield return config;
            }
        } 
        protected abstract TConfig CreateConfig(string configFile);

        protected abstract TPlugin CreatePlugin(TConfig config);
    }
}