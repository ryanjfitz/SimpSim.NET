using System.Windows.Controls;
using SimpSim.NET.Presentation.ViewModels;

namespace SimpSim.NET.WPF.Views
{
    /// <summary>
    /// Interaction logic for SystemRegisters.xaml
    /// </summary>
    public partial class SystemRegisters : UserControl
    {
        public SystemRegisters()
        {
            InitializeComponent();
            DataContext = new SystemRegistersViewModel(Globals.SimpleSimulator);
        }
    }
}
