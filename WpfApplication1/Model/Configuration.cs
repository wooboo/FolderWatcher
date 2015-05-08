using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using Newtonsoft.Json;

namespace FolderWatcher.Model
{
    [Export]
    public class Configuration
    {
        public Configuration()
        {
            Folders = JsonConvert.DeserializeObject<List<Directory>>(File.ReadAllText(@"config.json"));
        }
        public IList<Directory> Folders { get; set; }
    }
}
