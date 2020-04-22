using System.Runtime.CompilerServices;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class MemoryViewModel : ViewModelBase
    {
        private const string IndexerName = "Addresses";

        private readonly SimpleSimulator _simulator;

        public MemoryViewModel(SimpleSimulator simulator)
        {
            _simulator = simulator;

            _simulator.Memory.Changed += () => OnPropertyChanged(IndexerName);
        }

        [IndexerName(IndexerName)]
        public byte this[byte address]
        {
            get => _simulator.Memory[address];
            set => _simulator.Memory[address] = value;
        }
    }
}
