using SimpSim.NET.WPF.ViewModels;

namespace SimpSim.NET.WPF.Views
{
    /// <summary>
    /// Interaction logic for GeneralPurposeRegisters.xaml
    /// </summary>
    public partial class GeneralPurposeRegisters
    {
        public GeneralPurposeRegisters()
        {
            InitializeComponent();
            DataContext = new GeneralPurposeRegistersViewModel(App.SimpleSimulator);
        }
    }
}
