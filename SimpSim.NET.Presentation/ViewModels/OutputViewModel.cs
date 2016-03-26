using System.Windows.Input;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class OutputViewModel : ViewModelBase
    {
        private string _outputWindowText;

        public OutputViewModel(IWindowService windowService, SimpleSimulator simulator) : base(simulator)
        {
            OpenAssemblyEditorWindow = new Command(() => windowService.ShowAssemblyEditorWindow(new AssemblyEditorWindowViewModel(this, simulator)), () => true, simulator);
        }

        public string OutputWindowText
        {
            get
            {
                return _outputWindowText;
            }
            set
            {
                _outputWindowText = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenAssemblyEditorWindow { get; }
    }
}
