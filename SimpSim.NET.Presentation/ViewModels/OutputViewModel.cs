using System.Windows.Input;
using Prism.Mvvm;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class OutputViewModel : BindableBase
    {
        private string _outputWindowText;

        public OutputViewModel(SimpleSimulator simulator)
        {
            ClearCommand = new Command(() => OutputWindowText = null, () => true, simulator);

            // Needed for proper word wrap on XAML TextBlock.
            const string zeroWidthSpace = "\u200B";

            simulator.Registers.ValueWrittenToOutputRegister += c => OutputWindowText += c + zeroWidthSpace;
        }

        public string OutputWindowText
        {
            get => _outputWindowText;
            set => SetProperty(ref _outputWindowText, value);
        }

        public ICommand ClearCommand { get; }
    }
}