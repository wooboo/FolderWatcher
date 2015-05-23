using System.Collections.Generic;
using System.IO;
using System.Linq;
using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Core.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Plugins.Buttons
{
    public class ButtonsPlugin : PluginBase<ButtonsPluginConfig>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IEnumerable<IPlugin> _plugins;

        public ButtonsPlugin(ButtonsPluginConfig config, IEventAggregator eventAggregator) : base(config)
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
            var actions = Config.Buttons.Select(o => new FileAction
            {
                Path = file.FullPath,
                Plugin = o.Value,
                Name = o.Key
            });
            _eventAggregator.GetEvent<AddPluginPartEvent>().Publish(new AddPluginPart
            {
                Part = new ButtonsPluginPart(this, actions.ToList()),
                FilePath = file.FullPath,
                FolderPath = Path.GetDirectoryName(file.FullPath)
            });
        }

        public void Execute(FileAction fileViewModel)
        {
            _plugins.Single(o => o.Metadata.FullName == fileViewModel.Plugin)
                .OnFilesChange(new FileSystemChangeSet()
                {
                    Added = new List<FileChangeInfo> {new FileChangeInfo(fileViewModel.Path)}
                });
        }
    }
}