using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using FolderWatcher.Common.Model;
using Newtonsoft.Json;

namespace FolderWatcher.Model
{
    [Export]
    public class Configuration
    {
        public Configuration()
        {
            Folders = JsonConvert.DeserializeObject<List<DirectorySettings>>(File.ReadAllText(@"config.json"));
        }
        public IList<DirectorySettings> Folders { get; set; }
    }
}
