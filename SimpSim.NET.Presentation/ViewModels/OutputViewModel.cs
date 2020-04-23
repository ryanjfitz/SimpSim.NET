using System.Windows.Input;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class OutputViewModel : ViewModelBase
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
            set
            {
                _outputWindowText = value;
                OnPropertyChanged();
            }
        }

        public ICommand ClearCommand { get; }
    }
}