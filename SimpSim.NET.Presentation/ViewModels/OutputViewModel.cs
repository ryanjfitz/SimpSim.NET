namespace SimpSim.NET.Presentation.ViewModels
{
    public class OutputViewModel : ViewModelBase
    {
        private string _outputWindowText;

        public OutputViewModel(SimpleSimulator simulator) : base(simulator)
        {
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
    }
}