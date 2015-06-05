using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace FolderWatcher.Common.Model
{
    [Export]
    public class Configuration
    {
        IConfigurationStorage _configurationStorage;
        [ImportingConstructor]
        public Configuration(IConfigurationStorage configurationStorage)
        {
            _configurationStorage = configurationStorage;
        }
        public void Load()
        {
            Folders = _configurationStorage.LoadFolders();
        }
        public void Save()
        {
            _configurationStorage.SaveFolders(Folders);
        }
        public IList<DirectorySettings> Folders { get; set; }
    }
}