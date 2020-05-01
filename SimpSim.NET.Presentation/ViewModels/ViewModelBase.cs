using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpSim.NET.Presentation.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T member, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(member, value))
                return;

            member = value;

            OnPropertyChanged(propertyName);
        }
    }
}
