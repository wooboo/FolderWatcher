using System;
using System.Collections.Generic;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.Confirm
{
    public class ConfirmPluginConfig : PluginConfigBase
    {
        public ConfirmPluginConfig(string path) : base(path)
        {
        }

        public override string Description => "Confirmation Window Plugin";
        public string Title { get; set; } = "Confirm?";
        public string Text { get; set; } = "Do you really want to do it?";
        public string OkPlugin { get; set; } = "Null.default";
        public string CancelPlugin { get; set; } = "Null.default";
        public IList<string> CancelList { get; set; } = new List<string>();
    }
}