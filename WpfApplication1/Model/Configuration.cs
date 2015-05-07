using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfApplication1.Model
{
    [Export]
    public class Configuration
    {
        public Configuration()
        {
            Folders = JsonConvert.DeserializeObject<List<Folder>>(File.ReadAllText(@"config.json"));
        }
        public IList<Folder> Folders { get; set; }
    }

    public class Folder
    {
        public string Path { get; set; }
        public string Filter { get; set; }
        public IList<Plugin> Plugins { get; set; }
    }

    public class Plugin
    {
        public string Name { get; set; }
        public object Settings { get; set; }
    }
}
