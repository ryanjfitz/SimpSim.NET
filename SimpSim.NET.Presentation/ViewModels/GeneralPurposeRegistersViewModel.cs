using System.Runtime.CompilerServices;

namespace SimpSim.NET.Presentation.ViewModels
{
    public class GeneralPurposeRegistersViewModel : ViewModelBase
    {
        public GeneralPurposeRegistersViewModel()
        {
            ModelSingletons.Registers.CollectionChanged += (sender, e) => this[(byte)e.NewStartingIndex] = (byte)e.NewItems[0];
        }

        [IndexerName("GPR")]
        public byte this[byte register]
        {
            get
            {
                return ModelSingletons.Registers[register];
            }
            set
            {
                byte newValue = value;
                byte oldValue = ModelSingletons.Registers[register];

                if (newValue != oldValue)
                    ModelSingletons.Registers[register] = newValue;

                OnPropertyChanged();
            }
        }
    }
}
