using System.Runtime.CompilerServices;

namespace SimpSim.NET.WPF.ViewModels
{
    internal class MemoryViewModel : ViewModelBase
    {
        public MemoryViewModel()
        {
            Memory.CollectionChanged += (sender, e) => this[(byte)e.NewStartingIndex] = (byte)e.NewItems[0];
        }

        [IndexerName("Addresses")]
        public byte this[byte address]
        {
            get
            {
                return Memory[address];
            }
            set
            {
                Memory[address] = value;
                OnPropertyChanged();
            }
        }
    }
}
