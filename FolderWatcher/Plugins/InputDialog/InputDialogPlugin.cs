using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Common.Services;
using FolderWatcher.Core.Services;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Plugins.InputDialog
{
    public class InputDialogPlugin : PluginBase<InputDialogPluginConfig>
    {
        private readonly DispatcherTimer _dispatcherTimer;
        private IPlugin _okPlugin;
        private IPlugin _cancelPlugin;

        public InputDialogPlugin(InputDialogPluginConfig config) : base(config)
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

        public override void OnFilesChange(FileSystemChangeSet fileSystemChangeSet, IValueBag valueBag)
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
                var inputDialogPluginWindow = new InputDialogPluginWindow(Config, set);
                inputDialogPluginWindow.Owner = App.Current.MainWindow;
                if (inputDialogPluginWindow.ShowDialog()??false)
                {
                    valueBag.Values[Config.Key] = inputDialogPluginWindow.textBox.Text;
                    OkAction(set, valueBag);
                }
                else
                {
                    Cancel(set, valueBag);
                }
            });
        }

        public void Cancel(FileSystemChangeSet fileSystemChangeSet, IValueBag valueBag)
        {
            _cancelPlugin.OnFilesChange(fileSystemChangeSet, valueBag);
            foreach (var fileChangeInfo in fileSystemChangeSet.Added)
            {
                Config.CancelList.Add(fileChangeInfo.FullPath);
            }
            Config.Save();
        }
        public void OkAction(FileSystemChangeSet fileSystemChangeSet, IValueBag valueBag)
        {
            _okPlugin.OnFilesChange(fileSystemChangeSet, valueBag);
        }
    }
}