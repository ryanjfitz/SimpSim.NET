using SimpSim.NET.WPF.ViewModels;

namespace SimpSim.NET.WPF.Views
{
    /// <summary>
    /// Interaction logic for Memory.xaml
    /// </summary>
    public partial class Memory
    {
        public Memory()
        {
            InitializeComponent();
            DataContext = new MemoryViewModel(App.SimpleSimulator);
        }
    }
}
