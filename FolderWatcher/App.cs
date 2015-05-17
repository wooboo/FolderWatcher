using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName,
                    viewAssemblyName);
                viewModelName = viewModelName.Replace("ViewViewModel", "ViewModel");
                return Type.GetType(viewModelName);
            });
            ViewModelLocationProvider.SetDefaultViewModelFactory(
                type => { return ServiceLocator.Current.GetInstance(type); });
            // The boostrapper will create the Shell instance, so the App.xaml does not have a StartupUri.
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}