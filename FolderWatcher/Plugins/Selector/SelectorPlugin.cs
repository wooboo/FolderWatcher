using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FolderWatcher.Services;
using FolderWatcher.Services.Events;
using FolderWatcher.Watcher;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Plugins.Selector
{
    public class SelectorPlugin : PluginBase<SelectorPluginConfig>
    {
        private IDictionary<string, IEnumerable<IPlugin>> _plugins;

        public SelectorPlugin(SelectorPluginConfig config) : base(config)
        {
            _plugins = LoadPlugins();
        }
        private IDictionary<string, IEnumerable<IPlugin>> LoadPlugins()
        {
            var dict= new Dictionary<string, IEnumerable<IPlugin>>();
            var pluginManager = ServiceLocator.Current.GetInstance<PluginManager>();
            foreach (var mask in Config.Masks)
            {
                dict[mask.Key] = pluginManager.LoadAllPlugins(Config.GetPath(Config.GetName()), mask.Value);
            }
            return dict;
        }

        public override void OnFileCreated(FileChangeInfo file)
        {
            foreach (var masks in Config.Masks)
            {
                if (FitsMask(file.Name, masks.Key))
                {
                    foreach (var plugin in _plugins[masks.Key])
                    {
                        plugin.OnFileCreated(file);
                    }
                }

            }
        }


        private bool FitsMask(string sFileName, string sFileMask)
        {
            Regex mask = new Regex(sFileMask.Replace(".", "[.]").Replace("*", ".*").Replace("?", "."));
            return mask.IsMatch(sFileName);
        }
    }
}