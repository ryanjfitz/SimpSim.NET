using System.Windows.Input;

namespace SimpSim.NET.WPF.ViewModels
{
    internal class AssemblyEditorWindowViewModel : ViewModelBase
    {
        private string _assemblyEditorText;
        private string _assemblyErrorMessage;

        public AssemblyEditorWindowViewModel(OutputViewModel outputViewModel)
        {
            AssembleCommand = new Command(() =>
            {
                Instruction[] instructions = null;

                try
                {
                    instructions = Assembler.Assemble(AssemblyEditorText);
                }
                catch (AssemblyException ex)
                {
                    AssemblyErrorMessage = ex.Message;
                }

                if (instructions != null)
                {
                    Memory.LoadInstructions(instructions);

                    Registers.ValueWrittenToOutputRegister += c => outputViewModel.OutputWindowText += c;
                }
            }, () => true);
        }

        public string AssemblyEditorText
        {
            get
            {
                return _assemblyEditorText;
            }
            set
            {
                _assemblyEditorText = value;
                OnPropertyChanged();
            }
        }

        public string AssemblyErrorMessage
        {
            get
            {
                return _assemblyErrorMessage;
            }
            set
            {
                _assemblyErrorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand AssembleCommand { get; }
    }
}
