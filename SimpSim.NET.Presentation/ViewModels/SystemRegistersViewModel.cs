using System.Windows.Input;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class SystemRegistersViewModel : ViewModelBase
    {
        public SystemRegistersViewModel()
        {
            ResetProgramCounterCommand = new Command(() => Globals.Machine.ProgramCounter = 0x00, () => true);
        }

        public ICommand ResetProgramCounterCommand { get; }

        public byte ProgramCounter
        {
            get
            {
                return Globals.Machine.ProgramCounter;
            }
            set
            {
                Globals.Machine.ProgramCounter = value;
                OnPropertyChanged();
            }
        }

        public Instruction InstructionRegister => Globals.Machine.InstructionRegister;
    }
}
