using Prism.Mvvm;

namespace SimpSim.NET.WPF.ViewModels
{
    public class StatusBarViewModel : BindableBase
    {
        private Machine.MachineState _status;

        public StatusBarViewModel(SimpleSimulator simulator)
        {
            SetStatus(simulator);
            simulator.Machine.StateChanged += () => SetStatus(simulator);
        }

        private void SetStatus(SimpleSimulator simulator)
        {
            Status = simulator.Machine.State;
        }

        public Machine.MachineState Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }
    }
}