using System.Windows.Controls;
using SimpSim.NET.Presentation.ViewModels;

namespace SimpSim.NET.WPF.Views
{
    /// <summary>
    /// Interaction logic for Memory.xaml
    /// </summary>
    public partial class Memory : UserControl
    {
        public Memory()
        {
            InitializeComponent();
            DataContext = new MemoryViewModel(Globals.SimpleSimulator);
        }
    }
}
