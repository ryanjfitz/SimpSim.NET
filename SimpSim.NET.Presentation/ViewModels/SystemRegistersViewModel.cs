using System.Windows.Input;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class SystemRegistersViewModel : ViewModelBase
    {
        public SystemRegistersViewModel()
        {
            ResetProgramCounterCommand = new Command(() => ModelSingletons.Machine.ProgramCounter = 0x00, () => true);
        }

        public ICommand ResetProgramCounterCommand { get; }

        public byte ProgramCounter
        {
            get
            {
                return ModelSingletons.Machine.ProgramCounter;
            }
            set
            {
                ModelSingletons.Machine.ProgramCounter = value;
                OnPropertyChanged();
            }
        }

        public Instruction InstructionRegister => ModelSingletons.Machine.InstructionRegister;
    }
}
