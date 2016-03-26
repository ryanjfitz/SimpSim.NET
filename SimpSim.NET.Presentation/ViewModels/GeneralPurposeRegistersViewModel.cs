using System.Runtime.CompilerServices;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class GeneralPurposeRegistersViewModel : ViewModelBase
    {
        private readonly SimpleSimulator _simulator;

        public GeneralPurposeRegistersViewModel(SimpleSimulator simulator) : base(simulator)
        {
            _simulator = simulator;

            _simulator.Registers.CollectionChanged += (sender, e) => this[(byte)e.NewStartingIndex] = (byte)e.NewItems[0];
        }

        [IndexerName("GPR")]
        public byte this[byte register]
        {
            get
            {
                return _simulator.Registers[register];
            }
            set
            {
                byte newValue = value;
                byte oldValue = _simulator.Registers[register];

                if (newValue != oldValue)
                    _simulator.Registers[register] = newValue;

                OnPropertyChanged();
            }
        }
    }
}
