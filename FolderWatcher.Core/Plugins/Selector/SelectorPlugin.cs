using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Core.Services;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Core.Plugins.Selector
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
                dict[mask.Key] = pluginManager.LoadAllPlugins(Config.GetPath(Config.GetName()), mask.Value).ToList();
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