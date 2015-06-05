using System.Collections.Generic;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Core.Plugins.Sequence
{
    public class SequencePluginConfig : PluginConfigBase
    {
        public SequencePluginConfig(string path) : base(path)
        {
            
        }
        public override string Description=>"Sequential Plugin";
        public IList<string> Plugins { get; set; } = new List<string>();

    }
}