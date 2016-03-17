using System.Windows.Input;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class OutputViewModel : ViewModelBase
    {
        private string _outputWindowText;

        public OutputViewModel(IWindowService windowService)
        {
            OpenAssemblyEditorWindow = new Command(() => windowService.ShowAssemblyEditorWindow(new AssemblyEditorWindowViewModel(this)), () => true);
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
