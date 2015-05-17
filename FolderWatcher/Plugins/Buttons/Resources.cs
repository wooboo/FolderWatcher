using System.ComponentModel.Composition;
using System.Windows;

namespace FolderWatcher.Plugins.Buttons
{
    /// <summary>
    ///     Interaction logic for Resources.xaml
    /// </summary>
    [Export(typeof (ResourceDictionary))]
    public partial class Resources : ResourceDictionary
    {
        public Resources()
        {
            InitializeComponent();
        }
    }
}