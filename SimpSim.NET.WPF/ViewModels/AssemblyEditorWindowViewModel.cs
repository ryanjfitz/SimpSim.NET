using System.Windows.Input;

namespace SimpSim.NET.WPF.ViewModels
{
    internal class AssemblyEditorWindowViewModel : ViewModelBase
    {
        private string _assemblyEditorText;
        private string _assemblyResult;

        public AssemblyEditorWindowViewModel(OutputViewModel outputViewModel)
        {
            AssembleCommand = new Command(() =>
            {
                Instruction[] instructions = null;

                try
                {
                    instructions = Assembler.Assemble(AssemblyEditorText ?? "");
                    AssemblyResult = "Assembly Successful";
                }
                catch (AssemblyException ex)
                {
                    AssemblyResult = ex.Message;
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

        public string AssemblyResult
        {
            get
            {
                return _assemblyResult;
            }
            set
            {
                _assemblyResult = value;
                OnPropertyChanged();
            }
        }

        public ICommand AssembleCommand { get; }
    }
}
