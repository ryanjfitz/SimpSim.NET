using System.Windows.Controls;
using SimpSim.NET.Presentation.ViewModels;

namespace SimpSim.NET.WPF.Views
{
    /// <summary>
    /// Interaction logic for GeneralPurposeRegisters.xaml
    /// </summary>
    public partial class GeneralPurposeRegisters : UserControl
    {
        public GeneralPurposeRegisters()
        {
            InitializeComponent();
            DataContext = new GeneralPurposeRegistersViewModel(App.SimpleSimulator);
        }
    }
}
