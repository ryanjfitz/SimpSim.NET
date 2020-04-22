using System.ComponentModel;
using System.Windows.Controls;
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
            DataContext = new OutputViewModel(App.SimpleSimulator);

            SetupAutoScroll();
        }

        private void SetupAutoScroll()
        {
            DependencyPropertyDescriptor
                .FromProperty(TextBlock.TextProperty, typeof(TextBlock))
                .AddValueChanged(outputTextBlock, (sender, args) => outputScrollViewer.ScrollToEnd());
        }
    }
}
