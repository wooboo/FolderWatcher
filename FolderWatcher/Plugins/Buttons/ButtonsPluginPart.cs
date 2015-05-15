using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Input;
using FolderWatcher.Services;
using FolderWatcher.Shell;
using FolderWatcher.Watcher;
using Microsoft.Practices.Prism.Commands;

namespace FolderWatcher.Plugins.Buttons
{
    public class ButtonsPluginPart : IPluginPart {
        public IList<FileAction> Actions { get; set; }
        private readonly ButtonsPlugin _plugin;

        public ButtonsPluginPart(ButtonsPlugin plugin, IList<FileAction> actions)
        {
            Actions = actions;
            _plugin = plugin;
            ExecuteCommand = new DelegateCommand<FileAction>(Execute);
        }

        private async void Execute(FileAction fileAction)
        {
            _plugin.Execute(fileAction);
        }

        public ICommand ExecuteCommand { get; set; }
    }

    public class FileAction
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Plugin { get; set; }
    }
}