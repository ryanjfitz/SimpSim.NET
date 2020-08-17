﻿using System.Windows.Input;
using Prism.Mvvm;

namespace SimpSim.NET.WPF.ViewModels
{
    public class SystemRegistersViewModel : BindableBase
    {
        private readonly SimpleSimulator _simulator;

        public SystemRegistersViewModel(SimpleSimulator simulator)
        {
            _simulator = simulator;

            ResetProgramCounterCommand = new Command(() => _simulator.Machine.ProgramCounter = 0x00, () => true, _simulator);

            _simulator.Machine.ProgramCounterChanged += () => RaisePropertyChanged(nameof(ProgramCounter));
            _simulator.Machine.InstructionRegisterChanged += () => RaisePropertyChanged(nameof(InstructionRegister));
        }

        public ICommand ResetProgramCounterCommand { get; }

        public byte ProgramCounter
        {
            get => _simulator.Machine.ProgramCounter;
            set => _simulator.Machine.ProgramCounter = value;
        }

        public Instruction InstructionRegister => _simulator.Machine.InstructionRegister;
    }
}
