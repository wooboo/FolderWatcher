using System.Collections.Generic;
using System.Windows.Input;
using FolderWatcher.Model;
using Microsoft.Practices.Prism.Commands;

namespace FolderWatcher.Plugins.Buttons
{
    public class ButtonsPluginPart : IPluginPart
    {
        private readonly ButtonsPlugin _plugin;

        public ButtonsPluginPart(ButtonsPlugin plugin, IList<FileAction> actions)
        {
            Actions = actions;
            _plugin = plugin;
            ExecuteCommand = new DelegateCommand<FileAction>(Execute);
        }

        public IList<FileAction> Actions { get; set; }
        public ICommand ExecuteCommand { get; set; }

        private void Execute(FileAction fileAction)
        {
            _plugin.Execute(fileAction);
        }
    }
}