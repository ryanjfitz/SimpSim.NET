using System.Windows.Input;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class AssemblyEditorWindowViewModel : ViewModelBase
    {
        private string _assemblyEditorText;
        private string _assemblyResult;

        public AssemblyEditorWindowViewModel(SimpleSimulator simulator) : base(simulator)
        {
            AssembleCommand = new Command(() =>
            {
                Instruction[] instructions = null;

                try
                {
                    instructions = simulator.Assembler.Assemble(AssemblyEditorText ?? "");
                    AssemblyResult = "Assembly Successful";
                }
                catch (AssemblyException ex)
                {
                    AssemblyResult = ex.Message;
                }

                if (instructions != null)
                    simulator.Memory.LoadInstructions(instructions);
            }, () => true, simulator);
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
