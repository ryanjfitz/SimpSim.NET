using System.Windows.Input;

namespace SimpSim.NET.WPF.ViewModels
{
    internal class SystemRegistersViewModel : ViewModelBase
    {
        public SystemRegistersViewModel()
        {
            ResetProgramCounterCommand = new Command(() => Machine.ProgramCounter = 0x00, () => true);
        }

        public ICommand ResetProgramCounterCommand { get; }
    }
}
