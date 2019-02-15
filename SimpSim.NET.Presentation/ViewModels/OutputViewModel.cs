using System.Windows.Input;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class OutputViewModel : ViewModelBase
    {
        private string _outputWindowText;

        public OutputViewModel(IWindowService windowService, SimpleSimulator simulator) : base(simulator)
        {
            OpenAssemblyEditorWindow = new Command(() => windowService.ShowAssemblyEditorWindow(new AssemblyEditorWindowViewModel(simulator)), () => true, simulator);

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

        public ICommand OpenAssemblyEditorWindow { get; }
    }
}
