using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows;
using Microsoft.Practices.Prism.Mvvm;

namespace FolderWatcher.Shell
{
    /// <summary>
    ///     Interaction logic for ShellView.xaml
    /// </summary>
    [Export]
    public partial class ShellView : Window, IView, IPartImportsSatisfiedNotification
    {
        [ImportingConstructor]
        public ShellView([ImportMany] IEnumerable<ResourceDictionary> pluginResources)
        {
            foreach (var resourceDictionary in pluginResources)
            {
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }

            InitializeComponent();
            SetValue(
                ViewModelLocator.AutoWireViewModelProperty, true);
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

        public void OnImportsSatisfied()
        {
        }
    }
}