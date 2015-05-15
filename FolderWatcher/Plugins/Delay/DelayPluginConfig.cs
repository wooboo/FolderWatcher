using System;
using System.Collections.Generic;
using FolderWatcher.Plugins.Delay;

namespace FolderWatcher.Plugins.Delay
{
    public class DelayPluginConfig : PluginConfigBase
    {
        public DelayPluginConfig(string path) : base(path)
        {
        }

        public IList<FileState> FileStates { get; set; } = new List<FileState>();
        public TimeSpan DelayDelay { get; set; } = TimeSpan.FromHours(24);
        public string Plugin { get; set; }
    }
}