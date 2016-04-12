using System.Windows.Controls;
using SimpSim.NET.Presentation.ViewModels;

namespace SimpSim.NET.WPF.Views
{
    /// <summary>
    /// Interaction logic for MachineControls.xaml
    /// </summary>
    public partial class MachineControls : UserControl
    {
        public MachineControls()
        {
            InitializeComponent();
            DataContext = new MachineControlsViewModel(App.SimpleSimulator, new UserInputService(), new StateSaver());
        }
    }
}
