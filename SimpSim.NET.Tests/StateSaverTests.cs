using System;
using System.IO;
using Xunit;

namespace SimpSim.NET.Tests
{
    public class StateSaverTests
    {
        private readonly StateSaver _stateSaver;

        public StateSaverTests()
        {
            _stateSaver = new StateSaver();
        }

        [Fact]
        public void ShouldBeAbleToSaveMemoryState()
        {
            RunFileTest("MemorySaveFile.prg", file =>
            {
                Memory expectedMemory = new Memory();

                Random random = new Random();
                for (int address = 0; address <= byte.MaxValue; address++)
                    expectedMemory[(byte)address] = (byte)random.Next(0x00, byte.MaxValue);

                _stateSaver.SaveMemory(expectedMemory, file);

                Assert.True(file.Exists);

                Memory actualMemory = _stateSaver.LoadMemory(file);

                for (int address = 0; address <= byte.MaxValue; address++)
                    Assert.Equal(expectedMemory[(byte)address], actualMemory[(byte)address]);
            });
        }

        [Fact]
        public void ShouldBeAbleToSaveRegistersState()
        {
            RunFileTest("RegistersSaveFile.prg", file =>
            {
                Registers expectedRegisters = new Registers();

                Random random = new Random();
                for (byte register = 0; register <= 0x0F; register++)
                    expectedRegisters[register] = (byte)random.Next(0x00, byte.MaxValue);

                _stateSaver.SaveRegisters(expectedRegisters, file);

                Assert.True(file.Exists);

                Registers actualRegisters = _stateSaver.LoadRegisters(file);

                for (byte register = 0; register < 0x0F; register++)
                    Assert.Equal(expectedRegisters[register], actualRegisters[register]);
            });
        }

        [Fact]
        public void ShouldBeAbleToSaveMachineState()
        {
            RunFileTest("MachineSaveFile.prg", async file =>
            {
                Memory memory = new Memory();
                memory.LoadInstructions(SamplePrograms.HelloWorldInstructions);

                Machine expectedMachine = new Machine(memory, new Registers());
                await expectedMachine.RunAsync();

                _stateSaver.SaveMachine(expectedMachine, file);

                Assert.True(file.Exists);

                Machine actualMachine = _stateSaver.LoadMachine(file);

                Assert.Equal(expectedMachine.State, actualMachine.State);
                Assert.Equal(expectedMachine.InstructionRegister, actualMachine.InstructionRegister);
                Assert.Equal(expectedMachine.ProgramCounter, actualMachine.ProgramCounter);
            });
        }

        private void RunFileTest(string fileName, Action<FileInfo> testAction)
        {
            FileInfo file = null;

            try
            {
                file = new FileInfo(Path.Combine(Path.GetTempPath(), fileName));

                testAction(file);
            }
            finally
            {
                file?.Delete();
            }
        }
    }
}
