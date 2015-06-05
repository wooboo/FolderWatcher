using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace FolderWatcher.Common.Model
{
    [Export(typeof(IConfigurationStorage))]
    public class SettingsConfigurationStorage : IConfigurationStorage
    {
        public SettingsConfigurationStorage()
        {
            if (Properties.Settings.Default.Folders == null)
            {
                Properties.Settings.Default.Folders = new StringCollection();
                Properties.Settings.Default.Save();
            }
        }
        public IList<DirectorySettings> LoadFolders()
        {
            return Properties.Settings.Default.Folders.OfType<string>().Select(o => new DirectorySettings
            {
                Path = o
            }).ToList();
        }

        public void SaveFolders(IList<DirectorySettings> directories)
        {
            Properties.Settings.Default.Folders = new StringCollection();
            foreach (var directorySettingse in directories)
            {
                Properties.Settings.Default.Folders.Add(directorySettingse.Path);
            }
        }
    }
}