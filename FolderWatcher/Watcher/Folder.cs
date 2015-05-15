using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FolderWatcher.Model;
using FolderWatcher.Services;
using FolderWatcher.Services.Events;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Watcher
{

    public class Folder
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly FileSystemWatcher _fsw;
        private readonly string _path;
        private readonly string _configPath;
        private readonly IEnumerable<IPlugin> _plugins;

        public Folder(IFileSystemService fileSystemService, IPluginFactory[] plugins, DirectorySettings directorySettings)
        {
            _path = EnsureWatchedPath(directorySettings.Path);
            _configPath = EnsureConfigPath(_path);
            _plugins = LoadPlugins(_configPath, plugins).ToList();
            Files = new ObservableCollection<FileSystemItem>();

            _fileSystemService = fileSystemService;
            DirectorySettings = directorySettings;

            LoadFiles(_path);
            _fsw = CreateWatcher(_path);
        }

        private void LoadFiles(string path)
        {
            foreach (var file in System.IO.Directory.GetFiles(path))
            {
                AddFile(file);
            }
        }

        private IEnumerable<IPlugin> LoadPlugins(string configPath, IPluginFactory[] pluginFactories)
        {
            foreach (var pluginFactory in pluginFactories)
            {
                foreach (var plugin in pluginFactory.LoadPlugins(configPath))
                {
                    yield return plugin;
                }
            }
        }

        private string EnsureWatchedPath(string path)
        {
            path = Environment.ExpandEnvironmentVariables(path.Replace("~", "%USERPROFILE%"));

            return path;
        }

        private FileSystemWatcher CreateWatcher(string path)
        {
            var fsw = new FileSystemWatcher(path);
            //_fsw.Error += _fsw_Error;
            fsw.Deleted += _fsw_Deleted;
            fsw.Created += _fsw_Created;
            //_fsw.Changed += _fsw_Changed;
            return fsw;
        }

        private string EnsureConfigPath(string path)
        {
            var configPath = Path.Combine(path, ".watcher");
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
                var directoryInfo = new DirectoryInfo(configPath);
                directoryInfo.Attributes = directoryInfo.Attributes & FileAttributes.Hidden;
            }
            return configPath;
        }

        public Model.DirectorySettings DirectorySettings { get; private set; }
        public ObservableCollection<FileSystemItem> Files { get; set; }

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
            var changedFile = new FileSystemItem(path);
            foreach (var plugin in _plugins)
            {
                plugin.OnFile(changedFile);
            }
            App.Current.Dispatcher.Invoke(() => { Files.Add(changedFile); });
        }

        private void _fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            var file = Files.SingleOrDefault(o => o.FullPath == e.FullPath);
            if (file != null)
            {
                App.Current.Dispatcher.Invoke(() => { Files.Remove(file); });
            }
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

        public void Sweep()
        {
            foreach (var plugin in _plugins)
            {
                var periodialPlugin = plugin as IPeriodocalPlugin;
                if (periodialPlugin!=null)
                {
                    periodialPlugin.Sweep();
                }
            }
        }
    }
}