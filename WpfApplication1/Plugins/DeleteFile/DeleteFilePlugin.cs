using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO.IsolatedStorage;
using FolderWatcher.Services;
using FolderWatcher.Watcher;

namespace FolderWatcher.Plugins.DeleteFile
{
    [Export(typeof(IPlugin))]
    public class DeleteFilePlugin:IPlugin
    {
        
        public IList<FileState> Files { get; set; }

        public string Name { get { return GetType().Name; } }

        public void Init(dynamic settings)
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
       
        }

        public void OnFile(IFileSystemService fileSystemService, ChangedFile file)
        {
            file.PluginParts.Add(new DeleteFilePluginPart(fileSystemService, file));
        }

    }

    public class FileState
    {
        public string Path { get; set; }
        public DateTime CreateDate { get; set; }
        public TimeSpan DeleteAfter { get; set; }
    }
}