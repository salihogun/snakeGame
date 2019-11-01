using MahApps.Metro.Controls.Dialogs;
using Prism.Ioc;
using Prism.Mvvm;
using System.Windows;
using Znake.Desktop.Models;
using Znake.Desktop.ViewModels;
using Znake.Desktop.Views;

namespace Znake.Desktop
{
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDialogCoordinator, DialogCoordinator>();
            containerRegistry.RegisterSingleton<IRandom, MyRandom>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<ShellView, ShellViewModel>();
        }
    }
}
