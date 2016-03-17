using System.Runtime.CompilerServices;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class MemoryViewModel : ViewModelBase
    {
        public MemoryViewModel()
        {
            Globals.Memory.CollectionChanged += (sender, e) => this[(byte)e.NewStartingIndex] = (byte)e.NewItems[0];
        }

        [IndexerName("Addresses")]
        public byte this[byte address]
        {
            get
            {
                return Globals.Memory[address];
            }
            set
            {
                byte newValue = value;
                byte oldValue = Globals.Memory[address];

                if (newValue != oldValue)
                    Globals.Memory[address] = newValue;

                OnPropertyChanged();
            }
        }
    }
}
