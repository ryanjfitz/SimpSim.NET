using SimpSim.NET.WPF.ViewModels;

namespace SimpSim.NET.WPF.Views
{
    /// <summary>
    /// Interaction logic for MachineControls.xaml
    /// </summary>
    public partial class MachineControls
    {
        public MachineControls()
        {
            InitializeComponent();
            DataContext = new MachineControlsViewModel(App.SimpleSimulator, new UserInputService(), new WindowService(App.SimpleSimulator), new StateSaver());
        }
    }
}