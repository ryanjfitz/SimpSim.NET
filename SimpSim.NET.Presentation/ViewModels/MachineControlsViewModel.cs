using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class MachineControlsViewModel : ViewModelBase
    {
        public MachineControlsViewModel()
        {
            RunCommand = new Command(() => Task.Run(() => ModelSingletons.Machine.Run(25)), () => ModelSingletons.Machine.State != Machine.MachineState.Running);

            StepCommand = new Command(() => ModelSingletons.Machine.Step(), () => true);

            BreakCommand = new Command(() => ModelSingletons.Machine.Break(), () => ModelSingletons.Machine.State == Machine.MachineState.Running);

            ClearMemoryCommand = new Command(() => ModelSingletons.Memory.Clear(), () => true);

            ClearRegistersCommand = new Command(() => ModelSingletons.Registers.Clear(), () => true);
        }

        public ICommand RunCommand { get; }

        public ICommand StepCommand { get; }

        public ICommand BreakCommand { get; }

        public ICommand ClearMemoryCommand { get; }

        public ICommand ClearRegistersCommand { get; }
    }
}
