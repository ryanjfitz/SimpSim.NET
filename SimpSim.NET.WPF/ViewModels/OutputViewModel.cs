using System.Windows.Input;

namespace SimpSim.NET.WPF.ViewModels
{
    internal class OutputViewModel : ViewModelBase
    {
        private readonly IWindowService _windowService;
        private string _outputWindowText;

        public OutputViewModel(IWindowService windowService)
        {
            _windowService = windowService;

            OpenAssemblyEditorWindow = new Command(() => _windowService.ShowAssemblyEditorWindow(new AssemblyEditorWindowViewModel(this)), () => true);
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
