using System.Threading.Tasks;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class AssemblyEditorWindowViewModel : ViewModelBase
    {
        private bool _isAssembling;
        private string _assemblyEditorText;
        private string _assemblyResult;

        public AssemblyEditorWindowViewModel(SimpleSimulator simulator)
        {
            AssembleCommand = new Command(async () =>
            {
                Instruction[] instructions = null;

                try
                {
                    IsAssembling = true;
                    AssemblyResult = null;
                    instructions = await Task.Run(() => simulator.Assembler.Assemble(AssemblyEditorText)).ConfigureAwait(false);
                    AssemblyResult = "Assembly Successful";
                }
                catch (AssemblyException ex)
                {
                    AssemblyResult = ex.Message;
                }
                finally
                {
                    IsAssembling = false;
                }

                if (instructions != null)
                    simulator.Memory.LoadInstructions(instructions);
            }, () => !IsAssembling, simulator);
        }

        public bool IsAssembling
        {
            get => _isAssembling;
            set
            {
                SetProperty(ref _isAssembling, value);
                AssembleCommand.RaiseCanExecuteChanged();
            }
        }

        public string AssemblyEditorText
        {
            get => _assemblyEditorText;
            set => SetProperty(ref _assemblyEditorText, value);
        }

        public string AssemblyResult
        {
            get => _assemblyResult;
            set => SetProperty(ref _assemblyResult, value);
        }

        public Command AssembleCommand { get; }
    }
}