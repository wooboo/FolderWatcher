using System.Collections.Generic;
using System.Windows.Input;
using FolderWatcher.Common.Services;
using FolderWatcher.Model;
using Microsoft.Practices.Prism.Commands;

namespace FolderWatcher.Plugins.Buttons
{
    public class ButtonsPluginPart : IPluginPart
    {
        private readonly ButtonsPlugin _plugin;
        private readonly IValueBag _valueBag;

        public ButtonsPluginPart(ButtonsPlugin plugin, IList<FileAction> actions, IValueBag valueBag)
        {
            Actions = actions;
            _plugin = plugin;
            _valueBag = valueBag;
            ExecuteCommand = new DelegateCommand<FileAction>(Execute);
        }

        public IList<FileAction> Actions { get; set; }
        public ICommand ExecuteCommand { get; set; }

        private void Execute(FileAction fileAction)
        {
            _plugin.Execute(fileAction, _valueBag);
        }
    }
}