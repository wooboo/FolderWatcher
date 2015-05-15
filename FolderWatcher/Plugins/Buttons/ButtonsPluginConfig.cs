using System;
using System.Collections.Generic;
using FolderWatcher.Plugins.Delay;

namespace FolderWatcher.Plugins.Buttons
{
    public class ButtonsPluginConfig : PluginConfigBase
    {
        public ButtonsPluginConfig(string path) : base(path)
        {
        }

        public IList<FileState> FileStates { get; set; } = new List<FileState>();
        public TimeSpan ButtonsDelay { get; set; } = TimeSpan.FromHours(24);
    }
}