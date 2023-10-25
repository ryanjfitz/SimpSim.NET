using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SimpSim.NET.Assembly;

namespace SimpSim.NET.WPF.ViewModels;

public class AssemblyEditorDialogViewModel : BindableBase, IDialogAware
{
    private bool _canExecuteAssembleCommand;
    private string _assemblyEditorText;
    private string _assemblyError;

    public AssemblyEditorDialogViewModel(SimpleSimulator simulator)
    {
        CanExecuteAssembleCommand = true;
        AssembleCommand = new DelegateCommand(async () =>
        {
            if (await Assemble(simulator))
                RequestClose?.Invoke(new DialogResult());
        }).ObservesCanExecute(() => CanExecuteAssembleCommand);
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

    public string AssemblyError
    {
        get => _assemblyError;
        set => SetProperty(ref _assemblyError, value);
    }

    public async Task<bool> Assemble(SimpleSimulator simulator)
    {
        Instruction[] instructions = null;

        CanExecuteAssembleCommand = false;
        AssemblyError = null;

        try
        {
            instructions = await Task.Run(() => simulator.Assembler.Assemble(AssemblyEditorText)).ConfigureAwait(false);
        }
        catch (AssemblyException ex)
        {
            AssemblyError = $"Line {ex.LineNumber}: {ex.Message}";
        }
        finally
        {
            CanExecuteAssembleCommand = true;
        }

        if (instructions != null)
            simulator.Memory.LoadInstructions(instructions);

        return AssemblyError == null;
    }

    public ICommand AssembleCommand { get; }

    public bool CanCloseDialog() => true;

    public void OnDialogClosed() { }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        AssemblyEditorText = parameters.GetValue<string>("text");
    }

    public string Title => "Assembly Editor";

    public event Action<IDialogResult> RequestClose;
}