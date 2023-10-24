using System;
using System.IO;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace SimpSim.NET.WPF.ViewModels;

public class MachineControlsViewModel : BindableBase
{
    private readonly SimpleSimulator _simulator;

    public MachineControlsViewModel(SimpleSimulator simulator, IUserInputService userInputService, IDialogServiceAdapter dialogServiceAdapter, IStateSaver stateSaver)
    {
        _simulator = simulator;

        NewCommand = new DelegateCommand(() => dialogServiceAdapter.ShowAssemblyEditorDialog(), () => simulator.Machine.State != Machine.MachineState.Running);

        OpenCommand = new DelegateCommand(async () =>
        {
            FileInfo file = userInputService.GetOpenFileName();

            if (file != null)
            {
                if (file.Extension.Equals(".prg", StringComparison.OrdinalIgnoreCase))
                {
                    Memory memory = await stateSaver.LoadMemoryAsync(file);
                    simulator.Memory.LoadByteArray(memory.ToByteArray());
                }
                else if (file.Extension.Equals(".asm", StringComparison.OrdinalIgnoreCase))
                {
                    dialogServiceAdapter.ShowAssemblyEditorDialog(File.ReadAllText(file.FullName));
                }
            }
        }, () => simulator.Machine.State != Machine.MachineState.Running);

        SaveCommand = new DelegateCommand(async () =>
        {
            FileInfo file = userInputService.GetSaveFileName();

            if (file != null)
                await stateSaver.SaveMemoryAsync(simulator.Memory, file);
        }, () => simulator.Machine.State != Machine.MachineState.Running);

        RunCommand = new DelegateCommand(async () => await simulator.Machine.RunAsync(), () => simulator.Machine.State != Machine.MachineState.Running);

        StepCommand = new DelegateCommand(() => simulator.Machine.Step(), () => simulator.Machine.State != Machine.MachineState.Running);

        BreakCommand = new DelegateCommand(() => simulator.Machine.Break(), () => simulator.Machine.State == Machine.MachineState.Running);

        ClearMemoryCommand = new DelegateCommand(() => simulator.Memory.Clear());

        ClearRegistersCommand = new DelegateCommand(() => simulator.Registers.Clear());

        simulator.Machine.StateChanged += () =>
        {
            NewCommand.RaiseCanExecuteChanged();
            OpenCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            RunCommand.RaiseCanExecuteChanged();
            StepCommand.RaiseCanExecuteChanged();
            BreakCommand.RaiseCanExecuteChanged();
        };
    }

    public DelegateCommand NewCommand { get; }

    public DelegateCommand OpenCommand { get; }

    public DelegateCommand SaveCommand { get; }

    public DelegateCommand RunCommand { get; }

    public DelegateCommand StepCommand { get; }

    public DelegateCommand BreakCommand { get; }

    public ICommand ClearMemoryCommand { get; }

    public ICommand ClearRegistersCommand { get; }

    public int MillisecondsBetweenSteps
    {
        get => _simulator.Machine.MillisecondsBetweenSteps;
        set => _simulator.Machine.MillisecondsBetweenSteps = value;
    }
}