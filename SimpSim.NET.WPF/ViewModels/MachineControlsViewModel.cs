using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpSim.NET.WPF.ViewModels
{
    internal class MachineControlsViewModel : ViewModelBase
    {
        public MachineControlsViewModel()
        {
            RunCommand = new Command(() => Task.Run(() => Machine.Run(25)), () => true);

            StepCommand = new Command(() => Machine.Step(), () => true);

            BreakCommand = new Command(() => Machine.Break(), () => Machine.State == Machine.MachineState.Running);
        }

        public ICommand RunCommand { get; }

        public ICommand StepCommand { get; }

        public ICommand BreakCommand { get; }
    }
}
