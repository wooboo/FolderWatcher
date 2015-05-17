using System.Collections.Generic;
using System.Linq;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Core.Services;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Core.Plugins.Sequence
{
    public class SequencePlugin : PluginBase<SequencePluginConfig>
    {
        readonly IEnumerable<IPlugin> _plugins;
        public SequencePlugin(SequencePluginConfig config) : base(config)
        {
            _plugins = LoadPlugins();
        }

        private IEnumerable<IPlugin> LoadPlugins()
        {
            var pluginManager = ServiceLocator.Current.GetInstance<PluginManager>();
            return pluginManager.LoadAllPlugins(Config.GetPath(Config.GetName()), Config.Plugins).ToList();
        }

        public override void OnFileCreated(FileChangeInfo file)
        {
            foreach (var plugin in _plugins)
            {
                plugin.OnFileCreated(file);
            }
        }

    }
}