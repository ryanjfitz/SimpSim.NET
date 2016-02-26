using System.Windows.Input;

namespace SimpSim.NET.WPF.ViewModels
{
    internal class AssemblyEditorWindowViewModel : ViewModelBase
    {
        private string _assemblyEditorText;

        public AssemblyEditorWindowViewModel()
        {
            AssembleCommand = new Command(() =>
            {
                Instruction[] instructions = Assembler.Assemble(AssemblyEditorText);

                Memory.LoadInstructions(instructions);

                //Registers.ValueWrittenToOutputRegister += c => { };
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

        public ICommand AssembleCommand { get; }
    }
}
