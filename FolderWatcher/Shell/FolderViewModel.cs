using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Mvvm;

namespace FolderWatcher.Shell
{
    public class FolderViewModel : BindableBase
    {
        private ObservableCollection<FileViewModel> _files = new ObservableCollection<FileViewModel>();
        public string Name { get; set; }
        public string FullPath { get; set; }

        public ObservableCollection<FileViewModel> Files
        {
            get { return _files; }
            set { SetProperty(ref _files, value); }
        }
    }
}