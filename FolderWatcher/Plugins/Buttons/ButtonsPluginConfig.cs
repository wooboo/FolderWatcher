using System;
using System.Collections.Generic;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.Buttons
{
    public class ButtonsPluginConfig : PluginConfigBase
    {
        public ButtonsPluginConfig(string path) : base(path)
        {
        }
        public IDictionary<string, string> Buttons { get; set; } = new Dictionary<string, string>();

    }
}