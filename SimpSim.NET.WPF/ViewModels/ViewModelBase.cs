using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpSim.NET.WPF.ViewModels
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected static readonly Memory Memory = new Memory();

        protected static readonly Registers Registers = new Registers();

        protected static readonly Machine Machine = new Machine(Memory, Registers);

        protected static readonly Assembler Assembler = new Assembler();

        protected ViewModelBase()
        {
            Machine.PropertyChanged += (sender, e) => OnPropertyChanged(e.PropertyName);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
