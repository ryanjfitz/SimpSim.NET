using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class MachineControlsViewModel : ViewModelBase
    {
        public MachineControlsViewModel(SimpleSimulator simulator, IUserInputService userInputService, StateSaver stateSaver) : base(simulator)
        {
            OpenCommand = new Command(() =>
            {
                FileInfo file = userInputService.GetOpenFileName();

                if (file != null)
                    stateSaver.LoadMachine(file);
            }, () => true, simulator);

            SaveCommand = new Command(() =>
            {
                FileInfo file = userInputService.GetSaveFileName();

                if (file != null)
                    stateSaver.SaveMachine(simulator.Machine, file);
            }, () => true, simulator);

            RunCommand = new Command(() => Task.Run(() => simulator.Machine.Run(25)), () => simulator.Machine.State != Machine.MachineState.Running, simulator);

            StepCommand = new Command(() => simulator.Machine.Step(), () => true, simulator);

            BreakCommand = new Command(() => simulator.Machine.Break(), () => simulator.Machine.State == Machine.MachineState.Running, simulator);

            ClearMemoryCommand = new Command(() => simulator.Memory.Clear(), () => true, simulator);

            ClearRegistersCommand = new Command(() => simulator.Registers.Clear(), () => true, simulator);
        }

        public ICommand OpenCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand RunCommand { get; }

        public ICommand StepCommand { get; }

        public ICommand BreakCommand { get; }

        public ICommand ClearMemoryCommand { get; }

        public ICommand ClearRegistersCommand { get; }
    }
}
