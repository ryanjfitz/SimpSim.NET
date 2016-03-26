using System.Windows;
using SimpSim.NET.Presentation;

namespace SimpSim.NET.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly SimpleSimulator SimpleSimulator = new SimpleSimulator();
    }
}
