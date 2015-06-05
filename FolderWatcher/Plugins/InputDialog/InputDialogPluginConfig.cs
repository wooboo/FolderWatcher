using System;
using System.Collections.Generic;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Plugins.InputDialog
{
    public class InputDialogPluginConfig : PluginConfigBase
    {
        public InputDialogPluginConfig(string path) : base(path)
        {
        }

        public override string Description => "Input Dialog Window Plugin";
        public string Title { get; set; } = "Input Dialog";
        public string Key { get; set; } = "TEXT";
        public string Label { get; set; } = "Enter comment:";
        public string OkPlugin { get; set; } = "Null.default";
        public string CancelPlugin { get; set; } = "Null.default";
        public IList<string> CancelList { get; set; } = new List<string>();
    }
}