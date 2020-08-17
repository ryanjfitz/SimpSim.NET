using SimpSim.NET.WPF.ViewModels;

namespace SimpSim.NET.WPF.Views
{
    /// <summary>
    /// Interaction logic for SystemRegisters.xaml
    /// </summary>
    public partial class SystemRegisters
    {
        public SystemRegisters()
        {
            InitializeComponent();
            DataContext = new SystemRegistersViewModel(App.SimpleSimulator);
        }
    }
}
