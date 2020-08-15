using System;
using Prism.Commands;

namespace SimpSim.NET.Presentation
{
    public class Command : DelegateCommand
    {
        public Command(Action executeMethod, Func<bool> canExecuteMethod, SimpleSimulator simulator) : base(executeMethod, canExecuteMethod)
        {
            simulator.Machine.StateChanged += RaiseCanExecuteChanged;
        }
    }
}