using SimpSim.NET.Presentation.ViewModels;

namespace SimpSim.NET.WPF.Views
{
    /// <summary>
    /// Interaction logic for Output.xaml
    /// </summary>
    public partial class Output
    {
        public Output()
        {
            InitializeComponent();
            DataContext = new OutputViewModel(new WindowService(App.SimpleSimulator), App.SimpleSimulator);
        }
    }
}
