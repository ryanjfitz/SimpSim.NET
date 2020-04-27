using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpSim.NET.Presentation
{
    internal class Command : ICommand
    {
        protected readonly Action ExecuteAction;
        protected readonly Func<bool> CanExecuteFunc;

        public Command(Action executeAction, Func<bool> canExecuteFunc, SimpleSimulator simulator)
        {
            ExecuteAction = executeAction;
            CanExecuteFunc = canExecuteFunc;

            simulator.Machine.StateChanged += RaiseCanExecuteChanged;
        }

        public virtual bool CanExecute(object parameter)
        {
            return CanExecuteFunc.Invoke();
        }

        public virtual void Execute(object parameter)
        {
            ExecuteAction.Invoke();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        protected void RaiseCanExecuteChanged()
        {
            Application.Current?.Dispatcher?.Invoke(CommandManager.InvalidateRequerySuggested);
        }
    }

    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }

    internal class AsyncCommand : Command, IAsyncCommand
    {
        private bool _isExecuting;

        public AsyncCommand(Action executeAction, Func<bool> canExecuteFunc, SimpleSimulator simulator) : base(executeAction, canExecuteFunc, simulator)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return !_isExecuting && CanExecuteFunc.Invoke();
        }

        public override async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public async Task ExecuteAsync(object parameter)
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();

            await Task.Run(() => ExecuteAction.Invoke());

            _isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }
}
