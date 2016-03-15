using System.Runtime.CompilerServices;

namespace SimpSim.NET.WPF.ViewModels
{
    internal class GeneralPurposeRegistersViewModel : ViewModelBase
    {
        public GeneralPurposeRegistersViewModel()
        {
            Globals.Registers.CollectionChanged += (sender, e) => this[(byte)e.NewStartingIndex] = (byte)e.NewItems[0];
        }

        [IndexerName("GPR")]
        public byte this[byte register]
        {
            get
            {
                return Globals.Registers[register];
            }
            set
            {
                byte newValue = value;
                byte oldValue = Globals.Registers[register];

                if (newValue != oldValue)
                    Globals.Registers[register] = newValue;

                OnPropertyChanged();
            }
        }
    }
}
