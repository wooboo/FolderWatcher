using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Practices.ServiceLocation;
using WpfApplication1.Model;

namespace WpfApplication1.Watcher
{
    public class FolderWatcher
    {
        private string _path;
        FileSystemWatcher _fsw;
        public Folder Folder { get; private set; }
        private IList<IPlugin> _plugins = new List<IPlugin>();
        public ObservableCollection<ChangedFile> Files { get; set; }
        public FolderWatcher(string path, string filter = "*.*")
        {

            _path = Environment.ExpandEnvironmentVariables(path.Replace("~", "%USERPROFILE%"));
            _fsw = new FileSystemWatcher(_path);
            //_fsw.Error += _fsw_Error;
            //_fsw.Deleted += _fsw_Deleted;
            _fsw.Created += _fsw_Created;
            //_fsw.Changed += _fsw_Changed;
        }

        public FolderWatcher(Model.Folder folder):this(folder.Path, folder.Filter)
        {
            this.Folder = folder;
            foreach (var pluginSettings in folder.Plugins)
            {
                var plugin = ServiceLocator.Current.GetInstance<IPlugin>(pluginSettings.Name);
                _plugins.Add(plugin);
                plugin.Init(pluginSettings.Settings);
            }
        }

        void _fsw_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.Name);
        }


        void _fsw_Created(object sender, FileSystemEventArgs e)
        {
            var path = e.FullPath;
            foreach (var plugin in _plugins)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    path = plugin.OnFile(path);
                }
            }
        }

        void _fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.Name);
        }

        void _fsw_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.GetException());
        }
        public void Start()
        {
            _fsw.EnableRaisingEvents = true;
        }
        public void Stop()
        {
            _fsw.EnableRaisingEvents = false;
        }
    }

    public class ChangedFile
    {
        private readonly string _path;
        public string Name { get; set; }
        public ChangedFile(string path)
        {
            _path = path;
            Name = Path.GetFileName(_path);
        }
    }
}
