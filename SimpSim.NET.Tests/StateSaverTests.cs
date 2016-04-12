using System;
using System.IO;
using NUnit.Framework;

namespace SimpSim.NET.Tests
{
    [TestFixture]
    public class StateSaverTests
    {
        private readonly FileInfo _memorySaveFile = new FileInfo(Path.Combine(Path.GetTempPath(), "MemorySaveFile.bin"));
        private readonly FileInfo _registersSaveFile = new FileInfo(Path.Combine(Path.GetTempPath(), "RegistersSaveFile.bin"));
        private readonly FileInfo _machineSaveFile = new FileInfo(Path.Combine(Path.GetTempPath(), "MachineSaveFile.bin"));

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            DeleteSaveFiles();
        }

        [Test]
        public void ShouldBeAbleToSaveMemoryState()
        {
            Memory expectedMemory = new Memory();

            Random random = new Random();
            for (int address = 0; address <= byte.MaxValue; address++)
                expectedMemory[(byte)address] = (byte)random.Next(0x00, byte.MaxValue);

            StateSaver stateSaver = new StateSaver();
            stateSaver.SaveMemory(expectedMemory, _memorySaveFile);

            FileAssert.Exists(_memorySaveFile);

            Memory actualMemory = stateSaver.LoadMemory(_memorySaveFile);

            for (int address = 0; address <= byte.MaxValue; address++)
                Assert.AreEqual(expectedMemory[(byte)address], actualMemory[(byte)address]);
        }

        [Test]
        public void ShouldBeAbleToSaveRegistersState()
        {
            Registers expectedRegisters = new Registers();

            Random random = new Random();
            for (byte register = 0; register <= 0x0F; register++)
                expectedRegisters[register] = (byte)random.Next(0x00, byte.MaxValue);

            StateSaver stateSaver = new StateSaver();
            stateSaver.SaveRegisters(expectedRegisters, _registersSaveFile);

            FileAssert.Exists(_registersSaveFile);

            Registers actualRegisters = stateSaver.LoadRegisters(_registersSaveFile);

            for (byte register = 0; register < 0x0F; register++)
                Assert.AreEqual(expectedRegisters[register], actualRegisters[register]);
        }

        [Test]
        public void ShouldBeAbleToSaveMachineState()
        {
            Memory memory = new Memory();
            memory.LoadInstructions(SamplePrograms.HelloWorldInstructions);

            Machine expectedMachine = new Machine(memory, new Registers());
            expectedMachine.Run();

            StateSaver stateSaver = new StateSaver();
            stateSaver.SaveMachine(expectedMachine, _machineSaveFile);

            FileAssert.Exists(_machineSaveFile);

            Machine actualMachine = stateSaver.LoadMachine(_machineSaveFile);

            Assert.AreEqual(expectedMachine.State, actualMachine.State);
            Assert.AreEqual(expectedMachine.InstructionRegister, actualMachine.InstructionRegister);
            Assert.AreEqual(expectedMachine.ProgramCounter, actualMachine.ProgramCounter);
        }

        private void DeleteSaveFiles()
        {
            _memorySaveFile.Delete();
            _registersSaveFile.Delete();
            _machineSaveFile.Delete();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DeleteSaveFiles();
        }
    }
}
