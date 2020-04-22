using System.Runtime.CompilerServices;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class GeneralPurposeRegistersViewModel : ViewModelBase
    {
        private const string IndexerName = "GPR";

        private readonly SimpleSimulator _simulator;

        public GeneralPurposeRegistersViewModel(SimpleSimulator simulator)
        {
            _simulator = simulator;

            _simulator.Registers.Changed += () => OnPropertyChanged(IndexerName);
        }

        [IndexerName(IndexerName)]
        public byte this[byte register]
        {
            get => _simulator.Registers[register];
            set => _simulator.Registers[register] = value;
        }
    }
}
