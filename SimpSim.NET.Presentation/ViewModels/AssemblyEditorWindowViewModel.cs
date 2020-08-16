using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Mvvm;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class AssemblyEditorWindowViewModel : BindableBase
    {
        private bool _canExecuteAssembleCommand;
        private string _assemblyEditorText;
        private string _assemblyResult;

        public AssemblyEditorWindowViewModel(SimpleSimulator simulator)
        {
            CanExecuteAssembleCommand = true;
            AssembleCommand = new Command(async () => await Assemble(simulator), simulator).ObservesCanExecute(() => CanExecuteAssembleCommand);
        }

        public bool CanExecuteAssembleCommand
        {
            get => _canExecuteAssembleCommand;
            set => SetProperty(ref _canExecuteAssembleCommand, value);
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

        public async Task Assemble(SimpleSimulator simulator)
        {
            Instruction[] instructions = null;

            try
            {
                CanExecuteAssembleCommand = false;
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
                CanExecuteAssembleCommand = true;
            }

            if (instructions != null)
                simulator.Memory.LoadInstructions(instructions);
        }

        public ICommand AssembleCommand { get; }
    }
}