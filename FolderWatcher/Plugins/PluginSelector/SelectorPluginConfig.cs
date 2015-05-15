using System;
using System.Collections.Generic;
using FolderWatcher.Plugins.DeleteFile;

namespace FolderWatcher.Plugins.PluginSelector
{
    public class SelectorPluginConfig : PluginConfigBase
    {
        public SelectorPluginConfig(string path) : base(path)
        {
        }

        public IDictionary<string,IList<string>> Masks { get; set; } = new Dictionary<string, IList<string>>();
    }

}