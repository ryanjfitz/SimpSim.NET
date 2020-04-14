using System.Windows;
using SimpSim.NET.Presentation;
using SimpSim.NET.Presentation.ViewModels;
using SimpSim.NET.WPF.Views;

namespace SimpSim.NET.WPF
{
    internal class WindowService : IWindowService
    {
        private readonly SimpleSimulator _simulator;

        public WindowService(SimpleSimulator simulator)
        {
            _simulator = simulator;
        }

        public void ShowAssemblyEditorWindow(string text = null)
        {
            Window window = new AssemblyEditorWindow();
            window.DataContext = new AssemblyEditorWindowViewModel(_simulator) { AssemblyEditorText = text };
            window.Owner = Application.Current.MainWindow;
            window.Show();
        }
    }
}