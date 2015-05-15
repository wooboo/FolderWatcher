using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FolderWatcher.Model;
using FolderWatcher.Services;
using FolderWatcher.Services.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Watcher
{

    public class Folder
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IEventAggregator _eventAggregator;
        private readonly FileSystemWatcher _fsw;
        public string FullPath { get; }
        private readonly string _configPath;
        private readonly IEnumerable<IPlugin> _plugins;
        public string Name => Path.GetFileName(FullPath);

        public Folder(IFileSystemService fileSystemService, IPluginFactory[] plugins, IEventAggregator eventAggregator,  DirectorySettings directorySettings)
        {
            FullPath = EnsureWatchedPath(directorySettings.Path);
            _configPath = EnsureConfigPath(FullPath);
            _plugins = LoadPlugins(_configPath, plugins).ToList();
            Files = new ObservableCollection<FileChangeInfo>();

            _fileSystemService = fileSystemService;
            _eventAggregator = eventAggregator;
            DirectorySettings = directorySettings;

            _fsw = CreateWatcher(FullPath);
            _eventAggregator.GetEvent<FilesEvent>().Subscribe(OnFiles);
        }

        private void OnFiles(FileSystemChangeSet fileSystemChangeSet)
        {
            foreach (var fileChangeInfo in fileSystemChangeSet.Added)
            {
                foreach (var plugin in _plugins)
                {
                    plugin.OnFileCreated(fileChangeInfo);
                }
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
            fsw.Deleted += FileHandler;
            fsw.Created += FileHandler;
            fsw.Renamed += FileHandler;
            fsw.Changed += FileHandler;
            return fsw;
        }
        bool throttling = false;

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

        public DirectorySettings DirectorySettings { get; private set; }
        public ObservableCollection<FileChangeInfo> Files { get; set; }


        private IList<FileChangeInfo> _filesUnderChange = new List<FileChangeInfo>();
        private IList<FileChangeInfo> _created;
        private IList<string> _deleted;
        private async void FileHandler(object sender, FileSystemEventArgs e)
        {
            await OnChange();
        }



        private async Task OnChange()
        {
            if (!throttling)
            {
                throttling = true;
                await Task.Delay(TimeSpan.FromMilliseconds(250));

                _created = Directory.EnumerateFileSystemEntries(FullPath).Except(_filesUnderChange.Select(o => o.FullPath)).Select(o=>new FileChangeInfo(o)).ToList();
                await Task.Delay(TimeSpan.FromMilliseconds(250));
                throttling = false;
                foreach (var fileChangeInfo in _created)
                {
                    if (fileChangeInfo.HasChanged())
                    {
                        await OnChange();
                        break;
                    }
                }
                _created = Directory.EnumerateFileSystemEntries(FullPath).Except(_filesUnderChange.Select(o => o.FullPath)).Select(o => new FileChangeInfo(o)).ToList();
                _deleted = _filesUnderChange.Select(o => o.FullPath).Except(Directory.EnumerateFileSystemEntries(FullPath)).ToList();
                _eventAggregator.GetEvent<FilesEvent>().Publish(new FileSystemChangeSet()
                {
                    FolderPath = FullPath,
                    Added = _created,
                    Deleted = _deleted
                });
                _filesUnderChange = Directory.EnumerateFileSystemEntries(FullPath).Select(o => new FileChangeInfo(o)).ToList(); ;
            }
        }


        //private void AddFile(string path)
        //{
        //    var changedFile = new FileChangeInfo(path);
        //    foreach (var plugin in _plugins)
        //    {
        //        plugin.OnFileCreated(changedFile);
        //    }
        //    App.Current.Dispatcher.Invoke(() => { Files.Add(changedFile); });
        //}

        public async void Start()
        {
            await OnChange();
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
                if (periodialPlugin != null)
                {
                    periodialPlugin.Sweep();
                }
            }
        }
    }
}