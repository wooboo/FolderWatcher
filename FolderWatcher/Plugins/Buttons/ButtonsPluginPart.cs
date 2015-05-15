using System;
using System.Windows.Input;
using FolderWatcher.Services;
using FolderWatcher.Watcher;
using Microsoft.Practices.Prism.Commands;

namespace FolderWatcher.Plugins.Buttons
{
    public class ButtonsPluginPart : IPluginPart {
        private readonly ButtonsPlugin _plugin;
        public ChangedFile File { get; private set; }

        public ButtonsPluginPart(ButtonsPlugin plugin, ChangedFile file)
        {
            _plugin = plugin;
            File = file;
            Button = new DelegateCommand<ChangedFile>(Buttons);
        }

        private async void Buttons(ChangedFile changedFile)
        {
        }

        public ICommand Button { get; set; }
    }
}