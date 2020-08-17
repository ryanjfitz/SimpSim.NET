using System.ComponentModel;
using System.Windows.Controls;

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

            SetupAutoScroll();
        }

        private void SetupAutoScroll()
        {
            DependencyPropertyDescriptor
                .FromProperty(TextBlock.TextProperty, typeof(TextBlock))
                .AddValueChanged(OutputTextBlock, (sender, args) => OutputScrollViewer.ScrollToEnd());
        }
    }
}
