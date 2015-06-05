using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using Newtonsoft.Json;

namespace FolderWatcher.Common.Model
{
    //[Export(typeof(IConfigurationStorage))]
    public class FileConfigurationStorage : IConfigurationStorage
    {
        public IList<DirectorySettings> LoadFolders()
        {
            return JsonConvert.DeserializeObject<List<DirectorySettings>>(File.ReadAllText(@"config.json"));
        }

        public void SaveFolders(IList<DirectorySettings> directories)
        {
            File.WriteAllText(@"config.json", JsonConvert.SerializeObject(directories));
        }
    }
}