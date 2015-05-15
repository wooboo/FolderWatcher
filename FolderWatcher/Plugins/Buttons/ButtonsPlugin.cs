using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FolderWatcher.Services;
using FolderWatcher.Services.Events;
using FolderWatcher.Shell;
using FolderWatcher.Watcher;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Plugins.Buttons
{
    public class ButtonsPlugin : IPlugin
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ButtonsPluginConfig _config;

        public ButtonsPlugin(ButtonsPluginConfig config, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _config = config;
        }

        public void OnFileCreated(FileChangeInfo file)
        {
            var actions = _config.Buttons.Select(o => new FileAction()
            {
                Path = file.FullPath,
                Plugin = o.Value,
                Name = o.Key
            });
            _eventAggregator.GetEvent<AddPluginPartEvent>().Publish(new AddPluginPart()
            {
                Part = new ButtonsPluginPart(this, actions.ToList()),
                FilePath = file.FullPath,
                FolderPath = Path.GetDirectoryName(file.FullPath)
            });
        }

        public void OnFileDeleted(FileChangeInfo file)
        {
            
        }

        public void Execute(FileAction fileViewModel)
        {
            var factories = ServiceLocator.Current.GetAllInstances<IPluginFactory>();
            foreach (var pluginFactory in factories)
            {
                if (FitsMask(fileViewModel.Plugin, pluginFactory.Name + ".*.json"))
                {
                    //TODO: niez³e spaghetti :D
                    foreach (
                        var plugin in
                            pluginFactory.CreatePlugins(_config.GetPath("Buttons", fileViewModel.Plugin)))
                    {
                        plugin.OnFileCreated(new FileChangeInfo(fileViewModel.Path));
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