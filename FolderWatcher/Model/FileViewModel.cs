using System.Collections.Generic;
using System.IO;
using Microsoft.Practices.Prism.Mvvm;

namespace FolderWatcher.Model
{
    public class FileViewModel : BindableBase
    {
        public FileViewModel(string path)
        {
            FullPath = path;
            Name = Path.GetFileName(FullPath);
            PluginParts = new List<IPluginPart>();
        }

        public string FullPath { get; }
        public string Name { get; set; }
        public List<IPluginPart> PluginParts { get; set; }
    }
}