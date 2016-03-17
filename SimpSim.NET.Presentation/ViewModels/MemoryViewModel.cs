using System.Runtime.CompilerServices;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class MemoryViewModel : ViewModelBase
    {
        public MemoryViewModel()
        {
            ModelSingletons.Memory.CollectionChanged += (sender, e) => this[(byte)e.NewStartingIndex] = (byte)e.NewItems[0];
        }

        [IndexerName("Addresses")]
        public byte this[byte address]
        {
            get
            {
                return ModelSingletons.Memory[address];
            }
            set
            {
                byte newValue = value;
                byte oldValue = ModelSingletons.Memory[address];

                if (newValue != oldValue)
                    ModelSingletons.Memory[address] = newValue;

                OnPropertyChanged();
            }
        }
    }
}
