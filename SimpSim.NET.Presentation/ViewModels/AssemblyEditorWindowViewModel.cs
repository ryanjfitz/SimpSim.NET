namespace SimpSim.NET.Presentation.ViewModels
{
    public class AssemblyEditorWindowViewModel : ViewModelBase
    {
        private string _assemblyEditorText;
        private string _assemblyResult;

        public AssemblyEditorWindowViewModel(SimpleSimulator simulator)
        {
            AssembleCommand = new AsyncCommand(() =>
            {
                Instruction[] instructions = null;

                try
                {
                    instructions = simulator.Assembler.Assemble(AssemblyEditorText);
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
            get => _assemblyEditorText;
            set => SetProperty(ref _assemblyEditorText, value);
        }

        public string AssemblyResult
        {
            get => _assemblyResult;
            set => SetProperty(ref _assemblyResult, value);
        }

        public IAsyncCommand AssembleCommand { get; }
    }
}
