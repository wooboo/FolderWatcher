using System.ComponentModel.Composition;
using System.IO;
using System.Windows;

namespace WpfApplication1.Watcher
{
    [Export("DeleteFilePlugin",typeof(IPlugin))]
    public class DeleteFilePlugin:IPlugin
    {
        private bool _alert = false;
        public void Init(dynamic settings)
        {
            _alert = settings.Alert;
        }

        public string OnFile(string path)
        {
            if (_alert)
            {
                string message = "Are you sure you want to delete the file?";
                string caption = "Confirmation";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                var messageBoxResult = MessageBox.Show(message, caption, buttons, icon);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    File.Delete(path);
                    return null;
                }
                else
                {
                    return path;
                }
            }
            else
            {
                File.Delete(path);
                return null;
                
            }
        }
    }
}