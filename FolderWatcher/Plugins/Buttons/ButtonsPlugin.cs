using System;
using System.Collections.Generic;
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
    public class ButtonsPlugin : PluginBase<ButtonsPluginConfig> 
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IEnumerable<IPlugin> _plugins;

        public ButtonsPlugin(ButtonsPluginConfig config, IEventAggregator eventAggregator):base(config)
        {
            _eventAggregator = eventAggregator;
            _plugins = LoadPlugins();
        }

        private IEnumerable<IPlugin> LoadPlugins()
        { 
            var pluginManager = ServiceLocator.Current.GetInstance<PluginManager>();
            return pluginManager.LoadAllPlugins(Config.GetPath(Config.GetName()), Config.Buttons.Values).ToList();
        }

        public override void OnFileCreated(FileChangeInfo file)
        {
            var actions = Config.Buttons.Select(o => new FileAction()
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

        public void Execute(FileAction fileViewModel)
        {
            _plugins.Single(o => o.Name.FullName == fileViewModel.Plugin).OnFileCreated(new FileChangeInfo(fileViewModel.Path));
        }
    }
}