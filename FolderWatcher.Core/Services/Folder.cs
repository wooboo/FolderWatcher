using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Common.Services;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FolderWatcher.Core.Services
{
    public class Folder:IFolder
    {
        private readonly PluginManager _pluginManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly FileSystemWatcher _fsw;
        public string FullPath { get; }
        private readonly string _configPath;
        private readonly IEnumerable<IPlugin> _plugins;
        public string Name => Path.GetFileName(FullPath);

        public Folder(PluginManager pluginManager, IEventAggregator eventAggregator,  DirectorySettings directorySettings)
        {
            FullPath = EnsureWatchedPath(directorySettings.Path);
            _configPath = EnsureConfigPath(FullPath);
            _plugins = pluginManager.LoadAllPlugins(_configPath).ToList();

            _pluginManager = pluginManager;
            _eventAggregator = eventAggregator;
            DirectorySettings = directorySettings;

            _fsw = CreateWatcher(FullPath);
        }

        private void OnFiles(FileSystemChangeSet fileSystemChangeSet)
        {
                foreach (var plugin in _plugins)
                {
                    plugin.OnFilesChange(fileSystemChangeSet);
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
            }
            var directoryInfo = new DirectoryInfo(configPath);
            directoryInfo.Attributes = directoryInfo.Attributes | FileAttributes.Hidden;
            return configPath;
        }

        public DirectorySettings DirectorySettings { get; private set; }

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
                await Task.Delay(TimeSpan.FromMilliseconds(500));

                _created = Directory.EnumerateFileSystemEntries(FullPath).Except(_filesUnderChange.Select(o => o.FullPath)).Select(o=>new FileChangeInfo(o)).ToList();
                await Task.Delay(TimeSpan.FromMilliseconds(500));
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
                _filesUnderChange = Directory.EnumerateFileSystemEntries(FullPath).Select(o => new FileChangeInfo(o)).ToList(); ;
                Debug.WriteLine("Ok, we have file changes. {0} created, {1} deleted", _created.Count, _deleted.Count);
                var fileSystemChangeSet = new FileSystemChangeSet()
                {
                    FolderPath = FullPath,
                    Added = _created,
                    Deleted = _deleted
                };
                _eventAggregator.GetEvent<FilesEvent>().Publish(fileSystemChangeSet);
                OnFiles(fileSystemChangeSet);
            }
        }

        public void Start()
        {
            OnChange().ContinueWith(t => _fsw.EnableRaisingEvents = true);
        }

        public void Stop()
        {
            _fsw.EnableRaisingEvents = false;
        }

    }
}