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
        }

        public ICommand RunCommand { get; }

        public ICommand StepCommand { get; }

        public ICommand BreakCommand { get; }
    }
}
