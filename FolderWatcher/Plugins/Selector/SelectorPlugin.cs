using System.Text.RegularExpressions;
using FolderWatcher.Services.Events;
using FolderWatcher.Watcher;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Plugins.Selector
{
    public class SelectorPlugin : IPlugin
    {
        private readonly SelectorPluginConfig _config;

        public SelectorPlugin(SelectorPluginConfig config)
        {
            _config = config;
        }

        public void OnFileCreated(FileChangeInfo file)
        {
            var factories = ServiceLocator.Current.GetAllInstances<IPluginFactory>();
            foreach (var masks in _config.Masks)
            {
                if (FitsMask(file.Name, masks.Key))
                {
                    foreach (var pluginFactory in factories)
                    {
                        foreach (var subConfigFile in masks.Value)
                        {
                            if (FitsMask(subConfigFile, pluginFactory.Name + ".*.json"))
                            {
                                //TODO: niez³e spaghetti :D
                                foreach (
                                    var plugin in
                                        pluginFactory.CreatePlugins(_config.GetPath("Selector", subConfigFile)))
                                {
                                    plugin.OnFileCreated(file);
                                }
                            }
                        }
                    }
                }

            }
        }

        public void OnFileDeleted(FileChangeInfo file)
        {
            
        }

        private bool FitsMask(string sFileName, string sFileMask)
        {
            Regex mask = new Regex(sFileMask.Replace(".", "[.]").Replace("*", ".*").Replace("?", "."));
            return mask.IsMatch(sFileName);
        }
    }
}