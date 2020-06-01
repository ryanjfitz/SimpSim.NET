﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SimpSim.NET.Tests
{
    public class MachineTests
    {
        private readonly Memory _memory;
        private readonly Registers _registers;
        private readonly Machine _machine;

        public MachineTests()
        {
            _memory = new Memory();
            _registers = new Registers();
            _machine = new Machine(_memory, _registers);
        }

        [Fact]
        public void ProgramCounterShouldIncrementAfterStep()
        {
            _machine.ProgramCounter = 0x00;

            for (int i = 0; i <= byte.MaxValue; i++)
            {
                byte expected = (byte)(_machine.ProgramCounter + 0x02);
                _machine.Step();
                Assert.Equal(expected, _machine.ProgramCounter);
            }
        }

        [Fact]
        public void ShouldExecuteInstructionAtProgramCounterAddress()
        {
            Instruction instruction1 = new Instruction(0x93, 0x37);
            Instruction instruction2 = new Instruction(0x42, 0x21);
            Instruction instruction3 = new Instruction(0xAA, 0x14);
            Instruction instruction4 = new Instruction(0xBB, 0x08);

            _memory.LoadInstruction(instruction1, 0x00);
            _memory.LoadInstruction(instruction2, 0x02);
            _memory.LoadInstruction(instruction3, 0x04);
            _memory.LoadInstruction(instruction4, 0x06);

            _machine.ProgramCounter = 0x04;
            _machine.Step();

            Assert.Equal(instruction3, _machine.InstructionRegister);
        }

        [Fact]
        public void StateShouldChangeToRunningWhileRunning()
        {
            LaunchNonTerminatingProgram();

            Assert.Equal(Machine.MachineState.Running, _machine.State);
        }

        [Fact]
        public void ShouldBeAbleToBreakIfRunning()
        {
            Task task = LaunchNonTerminatingProgram();

            Assert.Equal(Machine.MachineState.Running, _machine.State);

            _machine.Break();

            // Wait for pending machine steps to execute.
            task.Wait();

            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void ShouldNotBeAbleToRunWhileAlreadyRunning()
        {
            LaunchNonTerminatingProgram();

            Assert.Equal(Machine.MachineState.Running, _machine.State);

            Assert.ThrowsAsync<InvalidOperationException>(() => _machine.RunAsync());
        }

        [Fact]
        public void ShouldNotBeAbleToBreakIfNotRunning()
        {
            Assert.Throws<InvalidOperationException>(() => _machine.Break());
        }

        private Task LaunchNonTerminatingProgram()
        {
            // Load a program that runs forever.
            _memory.LoadInstructions(SamplePrograms.OutputTestInstructions);

            Task task = Task.Run(() => _machine.RunAsync().Wait());

            while (task.Status != TaskStatus.Running)
            {
                // Wait until the task is running.
            }

            // Give the program time to begin executing.
            Thread.Sleep(100);

            return task;
        }
    }
}
