using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;
using Microsoft.Practices.Prism.Mvvm;

namespace FolderWatcher.Plugins.InputDialog
{
    /// <summary>
    /// Interaction logic for InputDialogPluginWindow.xaml
    /// </summary>
    public partial class InputDialogPluginWindow : Window
    {
        private readonly InputDialogPluginConfig _config;
        private FileSystemChangeSet _fileSystemChangeSet;

        public InputDialogPluginWindow(InputDialogPluginConfig config, FileSystemChangeSet fileSystemChangeSet) : this()
        {
            _config = config;
            this.Title = _config.Title;
            ShowInTaskbar = false;               // don't show the dialog on the taskbar
            Topmost = true;                      // ensure we're Always On Top
            ResizeMode = ResizeMode.NoResize;    // remove excess caption bar buttons
            Owner = Application.Current.MainWindow;
            textBlock.Text = _config.Label;
            _fileSystemChangeSet = fileSystemChangeSet;
        }


        public InputDialogPluginWindow()
        {
            InitializeComponent();

        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = false;
            Close();
        }
    }
}
