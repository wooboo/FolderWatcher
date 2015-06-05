using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Core.Services;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Plugins.Confirm
{
    public class ConfirmPlugin : PluginBase<ConfirmPluginConfig>
    {
        private readonly DispatcherTimer _dispatcherTimer;
        private IPlugin _okPlugin;
        private IPlugin _cancelPlugin;

        public ConfirmPlugin(ConfirmPluginConfig config) : base(config)
        {
            LoadPlugins();
        }

        private void LoadPlugins()
        {
            var pluginManager = ServiceLocator.Current.GetInstance<PluginManager>();
            var plugins = pluginManager.LoadAllPlugins(Config.GetPath(Config.GetName()), new[] {Config.OkPlugin, Config.CancelPlugin}).ToArray();
            _okPlugin = plugins[0];
            _cancelPlugin = plugins[1];
        }

        public override void OnFilesChange(FileSystemChangeSet fileSystemChangeSet)
        {
            var items = new List<FileChangeInfo>();
            foreach (var fileChangeInfo in fileSystemChangeSet.Added)
            {
                if (Config.CancelList.All(o => fileChangeInfo.FullPath != o))
                {
                    items.Add(fileChangeInfo);
                }
            }
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                var set = new FileSystemChangeSet()
                {
                    Added = items
                };
                var confirmPluginWindow = new ConfirmPluginWindow(Config, set);
                if (confirmPluginWindow.ShowDialog()??false)
                {
                    OkAction(set);
                }
                else
                {
                    Cancel(set);
                }
            });
        }

        public void Cancel(FileSystemChangeSet fileSystemChangeSet)
        {
            _cancelPlugin.OnFilesChange(fileSystemChangeSet);
            foreach (var fileChangeInfo in fileSystemChangeSet.Added)
            {
                Config.CancelList.Add(fileChangeInfo.FullPath);
            }
            Config.Save();
        }
        public void OkAction(FileSystemChangeSet fileSystemChangeSet)
        {
            _okPlugin.OnFilesChange(fileSystemChangeSet);
        }
    }
}