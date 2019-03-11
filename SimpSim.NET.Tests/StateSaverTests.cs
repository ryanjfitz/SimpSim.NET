using System;
using System.IO;
using NUnit.Framework;

namespace SimpSim.NET.Tests
{
    [TestFixture]
    public class StateSaverTests
    {
        private StateSaver _stateSaver;

        [SetUp]
        public void SetUp()
        {
            _stateSaver = new StateSaver();
        }

        [Test]
        public void ShouldBeAbleToSaveMemoryState()
        {
            RunFileTest("MemorySaveFile.prg", file =>
            {
                Memory expectedMemory = new Memory();

                Random random = new Random();
                for (int address = 0; address <= byte.MaxValue; address++)
                    expectedMemory[(byte)address] = (byte)random.Next(0x00, byte.MaxValue);

                _stateSaver.SaveMemory(expectedMemory, file);

                FileAssert.Exists(file);

                Memory actualMemory = _stateSaver.LoadMemory(file);

                for (int address = 0; address <= byte.MaxValue; address++)
                    Assert.AreEqual(expectedMemory[(byte)address], actualMemory[(byte)address]);
            });
        }

        [Test]
        public void ShouldBeAbleToSaveRegistersState()
        {
            RunFileTest("RegistersSaveFile.prg", file =>
            {
                Registers expectedRegisters = new Registers();

                Random random = new Random();
                for (byte register = 0; register <= 0x0F; register++)
                    expectedRegisters[register] = (byte)random.Next(0x00, byte.MaxValue);

                _stateSaver.SaveRegisters(expectedRegisters, file);

                FileAssert.Exists(file);

                Registers actualRegisters = _stateSaver.LoadRegisters(file);

                for (byte register = 0; register < 0x0F; register++)
                    Assert.AreEqual(expectedRegisters[register], actualRegisters[register]);
            });
        }

        [Test]
        public void ShouldBeAbleToSaveMachineState()
        {
            RunFileTest("MachineSaveFile.prg", file =>
            {
                Memory memory = new Memory();
                memory.LoadInstructions(SamplePrograms.HelloWorldInstructions);

                Machine expectedMachine = new Machine(memory, new Registers());
                expectedMachine.Run();

                _stateSaver.SaveMachine(expectedMachine, file);

                FileAssert.Exists(file);

                Machine actualMachine = _stateSaver.LoadMachine(file);

                Assert.AreEqual(expectedMachine.State, actualMachine.State);
                Assert.AreEqual(expectedMachine.InstructionRegister, actualMachine.InstructionRegister);
                Assert.AreEqual(expectedMachine.ProgramCounter, actualMachine.ProgramCounter);
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
