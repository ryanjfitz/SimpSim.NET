using System.Runtime.CompilerServices;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class GeneralPurposeRegistersViewModel : ViewModelBase
    {
        private readonly SimpleSimulator _simulator;

        public GeneralPurposeRegistersViewModel(SimpleSimulator simulator)
        {
            _simulator = simulator;

            _simulator.Registers.Changed += () => OnPropertyChanged("GPR");
        }

        [IndexerName("GPR")]
        public byte this[byte register]
        {
            get => _simulator.Registers[register];
            set
            {
                byte newValue = value;
                byte oldValue = _simulator.Registers[register];

                if (newValue != oldValue)
                    _simulator.Registers[register] = newValue;

                OnPropertyChanged("GPR");
            }
        }
    }
}
