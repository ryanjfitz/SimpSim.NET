using System.Runtime.CompilerServices;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class MemoryViewModel : ViewModelBase
    {
        private readonly SimpleSimulator _simulator;

        public MemoryViewModel(SimpleSimulator simulator)
        {
            _simulator = simulator;

            _simulator.Memory.Changed += () => OnPropertyChanged("Addresses");
        }

        [IndexerName("Addresses")]
        public byte this[byte address]
        {
            get => _simulator.Memory[address];
            set
            {
                byte newValue = value;
                byte oldValue = _simulator.Memory[address];

                if (newValue != oldValue)
                    _simulator.Memory[address] = newValue;

                OnPropertyChanged("Addresses");
            }
        }
    }
}
