using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FolderWatcher.Watcher;

namespace FolderWatcher.Services
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
