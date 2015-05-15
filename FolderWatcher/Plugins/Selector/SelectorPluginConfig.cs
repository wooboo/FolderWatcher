using System.Collections.Generic;

namespace FolderWatcher.Plugins.Selector
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

        public IDictionary<string, IList<string>> Masks { get; set; } = new Dictionary<string, IList<string>>();
    }

}