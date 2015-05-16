using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Core.Services
{
    [Export]
    public class PluginManager
    {
        private readonly IPluginFactory[] _pluginFactories;

        [ImportingConstructor]
        public PluginManager([ImportMany]IPluginFactory[] pluginFactories)
        {
            _pluginFactories = pluginFactories;
        }

        public IEnumerable<IPlugin> LoadAllPlugins(string path, IEnumerable<string> plugins=null)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var plugin1s = GetPlugins(path, plugins).ToList();
            if(plugins!=null)
            {
                plugin1s = (from p in plugins
                from s in plugin1s
                where s.Metadata.FullName == p
                select s).ToList();
            }
            return plugin1s.OrderBy(o=>o.Metadata.Rank).ToList();
        }

        private IEnumerable<IPlugin> GetPlugins(string path, IEnumerable<string> plugins)
        {
            foreach (var pluginFactory in _pluginFactories)
            {
                foreach (var plugin in pluginFactory.LoadPlugins(path, pluginFactory.FilterConfigs(plugins)))
                {
                    yield return plugin;
                }
            }
        }
    }
}
