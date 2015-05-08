using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FolderWatcher.Model;
using FolderWatcher.Services;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Watcher
{
    public class Folder
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly FileSystemWatcher _fsw;
        private readonly string _path;
        private readonly IList<IPlugin> _plugins = new List<IPlugin>();

        public Folder(string path, string filter = "*.*")
        {
            _path = Environment.ExpandEnvironmentVariables(path.Replace("~", "%USERPROFILE%"));
            _fsw = new FileSystemWatcher(_path);
            //_fsw.Error += _fsw_Error;
            //_fsw.Deleted += _fsw_Deleted;
            _fsw.Created += _fsw_Created;
            //_fsw.Changed += _fsw_Changed;
            Files = new ObservableCollection<ChangedFile>();
        }

        public Folder(IFileSystemService fileSystemService, IPlugin[] plugins, Model.Directory directory) : this(directory.Path, directory.Filter)
        {
            _fileSystemService = fileSystemService;
            Directory = directory;
            foreach (var pluginSettings in directory.Plugins)
            {
                var plugin = plugins.First(o=>o.Name == pluginSettings.Name);
                _plugins.Add(plugin);
                plugin.Init(pluginSettings.Settings);
            }
            foreach (var file in System.IO.Directory.GetFiles(_path))
            {
                AddFile(file);
            }
        }

        public Model.Directory Directory { get; private set; }
        public ObservableCollection<ChangedFile> Files { get; set; }

        private void _fsw_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.Name);
        }

        private void _fsw_Created(object sender, FileSystemEventArgs e)
        {
            var path = e.FullPath;
            AddFile(path);
        }

        private void AddFile(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                var changedFile = new ChangedFile(path);
                foreach (var plugin in _plugins)
                {
                    plugin.OnFile(_fileSystemService, changedFile);
                }
                App.Current.Dispatcher.Invoke(() => { Files.Add(changedFile); });
            }
        }

        private void _fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.Name);
        }

        private void _fsw_Error(object sender, ErrorEventArgs e)
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
}