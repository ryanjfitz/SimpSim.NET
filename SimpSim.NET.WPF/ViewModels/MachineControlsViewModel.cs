using System;
using System.IO;
using System.Windows.Input;
using Prism.Mvvm;

namespace SimpSim.NET.WPF.ViewModels
{
    public class MachineControlsViewModel : BindableBase
    {
        private readonly SimpleSimulator _simulator;

        public MachineControlsViewModel(SimpleSimulator simulator, IUserInputService userInputService, IWindowService windowService, StateSaver stateSaver)
        {
            _simulator = simulator;

            NewCommand = new Command(() => windowService.ShowAssemblyEditorWindow(), () => simulator.Machine.State != Machine.MachineState.Running, simulator);

            OpenCommand = new Command(() =>
            {
                FileInfo file = userInputService.GetOpenFileName();

                if (file != null)
                {
                    if (file.Extension.Equals(".prg", StringComparison.OrdinalIgnoreCase))
                    {
                        Memory memory = stateSaver.LoadMemory(file);
                        for (int i = 0; i <= byte.MaxValue; i++)
                            simulator.Memory[(byte)i] = memory[(byte)i];
                    }
                    else if (file.Extension.Equals(".asm", StringComparison.OrdinalIgnoreCase))
                    {
                        windowService.ShowAssemblyEditorWindow(File.ReadAllText(file.FullName));
                    }
                }
            }, () => simulator.Machine.State != Machine.MachineState.Running, simulator);

            SaveCommand = new Command(() =>
            {
                FileInfo file = userInputService.GetSaveFileName();

                if (file != null)
                    stateSaver.SaveMemory(simulator.Memory, file);
            }, () => simulator.Machine.State != Machine.MachineState.Running, simulator);

            RunCommand = new Command(async () => await simulator.Machine.RunAsync(), () => simulator.Machine.State != Machine.MachineState.Running, simulator);

            StepCommand = new Command(() => simulator.Machine.Step(), () => true, simulator);

            BreakCommand = new Command(() => simulator.Machine.Break(), () => simulator.Machine.State == Machine.MachineState.Running, simulator);

            ClearMemoryCommand = new Command(() => simulator.Memory.Clear(), () => true, simulator);

            ClearRegistersCommand = new Command(() => simulator.Registers.Clear(), () => true, simulator);
        }

        public ICommand NewCommand { get; }

        public ICommand OpenCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand RunCommand { get; }

        public ICommand StepCommand { get; }

        public ICommand BreakCommand { get; }

        public ICommand ClearMemoryCommand { get; }

        public ICommand ClearRegistersCommand { get; }

        public int MillisecondsBetweenSteps
        {
            get => _simulator.Machine.MillisecondsBetweenSteps;
            set => _simulator.Machine.MillisecondsBetweenSteps = value;
        }
    }
}