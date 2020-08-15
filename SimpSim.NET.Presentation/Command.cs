using System;
using System.Threading;
using System.Windows.Input;

namespace SimpSim.NET.Presentation
{
    public class Command : ICommand
    {
        private readonly Action _executeAction;
        private readonly Func<bool> _canExecuteFunc;
        private readonly SynchronizationContext _synchronizationContext;

        public Command(Action executeAction, Func<bool> canExecuteFunc, SimpleSimulator simulator)
        {
            _executeAction = executeAction;
            _canExecuteFunc = canExecuteFunc;
            _synchronizationContext = SynchronizationContext.Current;

            simulator.Machine.StateChanged += RaiseCanExecuteChanged;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc.Invoke();
        }

        public void Execute(object parameter)
        {
            _executeAction.Invoke();
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                if (_synchronizationContext != null && _synchronizationContext != SynchronizationContext.Current)
                    _synchronizationContext.Post((o) => handler.Invoke(this, EventArgs.Empty), null);
                else
                    handler.Invoke(this, EventArgs.Empty);
            }
        }
    }
}