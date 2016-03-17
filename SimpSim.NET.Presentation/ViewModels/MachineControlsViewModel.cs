using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class MachineControlsViewModel : ViewModelBase
    {
        public MachineControlsViewModel()
        {
            RunCommand = new Command(() => Task.Run(() => Globals.Machine.Run(25)), () => Globals.Machine.State != Machine.MachineState.Running);

            StepCommand = new Command(() => Globals.Machine.Step(), () => true);

            BreakCommand = new Command(() => Globals.Machine.Break(), () => Globals.Machine.State == Machine.MachineState.Running);
        }

        public ICommand RunCommand { get; }

        public ICommand StepCommand { get; }

        public ICommand BreakCommand { get; }
    }
}
