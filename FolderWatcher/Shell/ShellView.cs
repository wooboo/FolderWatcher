using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows;
using FolderWatcher.Model;
using Microsoft.Practices.Prism.Mvvm;

namespace FolderWatcher.Shell
{
    /// <summary>
    ///     Interaction logic for ShellView.xaml
    /// </summary>
    [Export]
    public partial class ShellView : Window, IView
    {
        [ImportingConstructor]
        public ShellView([ImportMany] IEnumerable<ResourceDictionary> pluginResources)
        {
            foreach (var resourceDictionary in pluginResources)
            {
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }

            InitializeComponent();
            SetValue(ViewModelLocator.AutoWireViewModelProperty, true);
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            var i = new Icon(Application.GetResourceStream(new Uri("/eye.ico", UriKind.Relative)).Stream);
            ni.Icon = i;
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var viewModel = DataContext as ShellViewModel;
            if (viewModel != null)
            {
                viewModel.SelectedFolder = e.NewValue as FolderViewModel;
            }
        }
    }
}