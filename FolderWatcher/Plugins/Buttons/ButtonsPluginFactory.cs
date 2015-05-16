using System.ComponentModel.Composition;
using System.IO;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Newtonsoft.Json;

namespace FolderWatcher.Plugins.Buttons
{
    [Export(typeof(IPluginFactory))]
    public class ButtonsPluginFactory : PluginFactoryBase<ButtonsPlugin, ButtonsPluginConfig>
    {
        private readonly IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public ButtonsPluginFactory(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        protected override ButtonsPluginConfig CreateConfig(string configFile)
        {
            return new ButtonsPluginConfig(configFile);
        }

        protected override ButtonsPlugin CreatePlugin(ButtonsPluginConfig config)
        {
            return new ButtonsPlugin(config, _eventAggregator);
        }
    }
}