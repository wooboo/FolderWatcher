using System.Collections.Generic;

namespace FolderWatcher.Common.Model
{
    public interface IConfigurationStorage
    {
        IList<DirectorySettings> LoadFolders();
        void SaveFolders(IList<DirectorySettings> directories);
    }
}