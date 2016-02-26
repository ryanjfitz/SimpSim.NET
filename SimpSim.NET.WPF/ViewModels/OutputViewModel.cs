using System.Windows.Input;

namespace SimpSim.NET.WPF.ViewModels
{
    internal class OutputViewModel : ViewModelBase
    {
        private readonly IWindowService _windowService;

        public OutputViewModel(IWindowService windowService)
        {
            _windowService = windowService;

            OpenAssemblyEditorWindow = new Command(() => _windowService.ShowAssemblyEditorWindow(new AssemblyEditorWindowViewModel()), () => true);
        }

        public ICommand OpenAssemblyEditorWindow { get; }
    }
}
