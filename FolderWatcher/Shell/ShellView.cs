using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Mvvm;
using ResourceDictionary = System.Windows.ResourceDictionary;

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
            this.SetValue(
                ViewModelLocator.AutoWireViewModelProperty, true);
            
        }

        public void OnImportsSatisfied()
        {
            
        }
    }

}