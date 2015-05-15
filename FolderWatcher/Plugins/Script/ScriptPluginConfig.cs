using System.Collections.Generic;

namespace FolderWatcher.Plugins.Script
{
    /// <summary>
    /// <example>
    /// {
    ///   "fileName": "cmd.exe",
    ///   "arguments": "/C move "\{0}\" C:\\Users\\woobo\\Downloads\\pdf" 
    /// }
    /// </example>
    /// </summary>
    public class ScriptPluginConfig : PluginConfigBase
    {
        public ScriptPluginConfig(string path) : base(path)
        {
        }

        public string Arguments { get; set; }
        public string FileName { get; set; }
        public string Script { get; set; }
    }

}