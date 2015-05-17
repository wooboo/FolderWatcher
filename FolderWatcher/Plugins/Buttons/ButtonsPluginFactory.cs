using System.ComponentModel.Composition;
using FolderWatcher.Common.Plugins;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FolderWatcher.Plugins.Buttons
{
    [Export(typeof (IPluginFactory))]
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