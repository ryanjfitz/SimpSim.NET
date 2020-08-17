using System;
using Prism.Commands;

namespace SimpSim.NET.WPF
{
    internal class Command : DelegateCommand
    {
        public Command(Action executeMethod, Func<bool> canExecuteMethod, SimpleSimulator simulator) : base(executeMethod, canExecuteMethod)
        {
            simulator.Machine.StateChanged += RaiseCanExecuteChanged;
        }

        public Command(Action executeMethod, SimpleSimulator simulator) : this(executeMethod, () => true, simulator)
        {
        }
    }
}