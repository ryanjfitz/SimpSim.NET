using System.Windows.Input;

namespace SimpSim.NET.WPF.ViewModels
{
    internal class MachineControlsViewModel : ViewModelBase
    {
        public MachineControlsViewModel()
        {
            RunCommand = new Command(() => Machine.Run(), () => true);

            StepCommand = new Command(() => Machine.Step(), () => true);
        }

        public ICommand RunCommand { get; }

        public ICommand StepCommand { get; }
    }
}
