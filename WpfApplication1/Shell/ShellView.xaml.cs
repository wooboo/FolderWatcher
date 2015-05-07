using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.Mvvm;

namespace WpfApplication1.Shell
{
    /// <summary>
    ///     Interaction logic for ShellView.xaml
    /// </summary>
    [Export]
    public partial class ShellView : Window, IView, IPartImportsSatisfiedNotification
    {
        public ShellView()
        {
            InitializeComponent();
            this.SetValue(
                ViewModelLocator.AutoWireViewModelProperty, true);
            
        }

        public void OnImportsSatisfied()
        {
            
        }
    }
}