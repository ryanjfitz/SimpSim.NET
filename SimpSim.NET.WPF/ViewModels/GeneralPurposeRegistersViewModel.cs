using System.Runtime.CompilerServices;

namespace SimpSim.NET.WPF.ViewModels
{
    internal class GeneralPurposeRegistersViewModel : ViewModelBase
    {
        public GeneralPurposeRegistersViewModel()
        {
            Registers.CollectionChanged += (sender, e) => this[(byte)e.NewStartingIndex] = (byte)e.NewItems[0];
        }

        [IndexerName("Registers")]
        public byte this[byte register]
        {
            get
            {
                return Registers[register];
            }
            set
            {
                byte newValue = value;
                byte oldValue = Registers[register];

                if (newValue != oldValue)
                    Registers[register] = newValue;

                OnPropertyChanged();
            }
        }
    }
}
