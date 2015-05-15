using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FolderWatcher.Plugins.Buttons
{
    public class AddPluginPartEvent : PubSubEvent<AddPluginPart>
    {
    }

    public class AddPluginPart
    {
        public ButtonsPluginPart Part { get; set; }
        public string FolderPath { get; set; }
        public string FilePath { get; set; }
    }
}
