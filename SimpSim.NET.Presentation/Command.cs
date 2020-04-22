using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpSim.NET.Presentation
{
    internal class Command : ICommand
    {
        private readonly Action _executeAction;
        private readonly Func<bool> _canExecuteFunc;

        public Command(Action executeAction, Func<bool> canExecuteFunc, SimpleSimulator simulator)
        {
            _executeAction = executeAction;
            _canExecuteFunc = canExecuteFunc;

            simulator.Machine.StateChanged += OnCanExecuteChanged;
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
            Application.Current?.Dispatcher?.Invoke(() => CanExecuteChanged?.Invoke(this, EventArgs.Empty));
        }
    }

    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }

    internal class AsyncCommand : IAsyncCommand
    {
        private bool _isExecuting;
        private readonly Action _executeAction;
        private readonly Func<bool> _canExecuteFunc;

        public AsyncCommand(Action executeAction, Func<bool> canExecuteFunc, SimpleSimulator simulator)
        {
            _executeAction = executeAction;
            _canExecuteFunc = canExecuteFunc;

            simulator.Machine.StateChanged += OnCanExecuteChanged;
        }

        public bool CanExecute(object parameter)
        {
            return !_isExecuting && _canExecuteFunc.Invoke();
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public async Task ExecuteAsync(object parameter)
        {
            _isExecuting = true;
            OnCanExecuteChanged();

            await Task.Run(() => _executeAction.Invoke());

            _isExecuting = false;
            OnCanExecuteChanged();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private void OnCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
