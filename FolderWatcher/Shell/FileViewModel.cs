using System.Collections.Generic;
using System.IO;
using FolderWatcher.Services;
using Microsoft.Practices.Prism.Mvvm;

namespace FolderWatcher.Shell
{
    public class FileViewModel:BindableBase
    {
        public FileViewModel(string path)
        {
            FullPath = path;
            Name = Path.GetFileName(FullPath);
            PluginParts = new List<IPluginPart>();
        }

        public string FullPath { get; private set; }
        public string Name { get; set; }
        public List<IPluginPart> PluginParts { get; set; }
    }
}