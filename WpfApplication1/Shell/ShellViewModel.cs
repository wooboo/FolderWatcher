using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using WpfApplication1.Services;
using WpfApplication1.Watcher;

namespace WpfApplication1.Shell
{
    [Export]
    public class ShellViewModel:BindableBase
    {
        private readonly ISomeService _someService;
        private ObservableCollection<FolderWatcher> _watchers;

        [ImportingConstructor]
        public ShellViewModel(ISomeService someService)
        {
            _someService = someService;
            this.Watchers = _someService.Watchers;
        }

        public ObservableCollection<FolderWatcher> Watchers
        {
            get { return _watchers; }
            set { SetProperty(ref _watchers, value); }
        }

    }
}
