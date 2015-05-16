using System;
using System.Collections.Generic;

namespace FolderWatcher.Plugins.Null
{
    public class NullPluginConfig : PluginConfigBase
    {
        public NullPluginConfig(string path) : base(path)
        {
        }

        public override void Save()
        {
        }

        public override bool Load()
        {
            return true;
        }
    }
}