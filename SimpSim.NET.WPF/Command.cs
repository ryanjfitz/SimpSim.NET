using System;
using System.Windows.Input;

namespace SimpSim.NET.WPF
{
    internal class Command : ICommand
    {
        private readonly Action _executeAction;
        private readonly Func<bool> _canExecuteFunc;

        public Command(Action executeAction, Func<bool> canExecuteFunc)
        {
            _executeAction = executeAction;
            _canExecuteFunc = canExecuteFunc;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc.Invoke();
        }

        public void Execute(object parameter)
        {
            _executeAction.Invoke();
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
