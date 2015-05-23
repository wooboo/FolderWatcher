using System.Collections.Generic;
using FolderWatcher.Common.Plugins;

namespace FolderWatcher.Core.Plugins.Selector
{
    /// <summary>
    /// <example>
    /// {
    ///   "masks": {
    ///     "*.pdf": ["ScriptPlugin.pdf.json"]
    ///   }
    /// }
    /// </example>
    /// </summary>
    public class SelectorPluginConfig : PluginConfigBase
    {
        public SelectorPluginConfig(string path) : base(path)
        {
        }

        public IDictionary<string, string> Masks { get; set; } = new Dictionary<string, string>();
    }

}