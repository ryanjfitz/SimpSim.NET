using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpSim.NET.WPF.ViewModels
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected ViewModelBase()
        {
            Globals.Machine.PropertyChanged += (sender, e) => OnPropertyChanged(e.PropertyName);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
