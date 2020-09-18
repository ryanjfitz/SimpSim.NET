using System.Windows;
using Prism.Ioc;
using SimpSim.NET.WPF.ViewModels;
using SimpSim.NET.WPF.Views;

namespace SimpSim.NET.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            DispatcherUnhandledException += (sender, e) =>
            {
                MessageBox.Show(e.Exception.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            };
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<SimpleSimulator>();
            containerRegistry.Register<IUserInputService, UserInputService>();
            containerRegistry.Register<IDialogServiceAdapter, DialogServiceAdapter>();
            containerRegistry.RegisterSingleton<IStateSaver, StateSaver>();
            containerRegistry.RegisterDialog<AssemblyEditorDialog, AssemblyEditorDialogViewModel>();
        }
    }
}
