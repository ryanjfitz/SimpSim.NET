using System.Windows.Input;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class OutputViewModel : ViewModelBase
    {
        private string _outputWindowText;

        public OutputViewModel(SimpleSimulator simulator) : base(simulator)
        {
            ClearCommand = new Command(() => OutputWindowText = null, () => true, simulator);

            simulator.Registers.ValueWrittenToOutputRegister += c => OutputWindowText += c;
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