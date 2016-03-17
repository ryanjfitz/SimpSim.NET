using System;
using System.Windows;
using System.Windows.Input;

namespace SimpSim.NET.Presentation
{
    internal class Command : ICommand
    {
        private readonly Action _executeAction;
        private readonly Func<bool> _canExecuteFunc;

        public Command(Action executeAction, Func<bool> canExecuteFunc)
        {
            _executeAction = executeAction;
            _canExecuteFunc = canExecuteFunc;

            ModelSingletons.Machine.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "State")
                    OnCanExecuteChanged();
            };
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

        private void OnCanExecuteChanged()
        {
            Application.Current.Dispatcher.Invoke(() => CanExecuteChanged?.Invoke(this, EventArgs.Empty));
        }
    }
}
