using System;
using System.IO;
using NUnit.Framework;

namespace SimpSim.NET.Tests
{
    [TestFixture]
    public class SerializationTests
    {
        private readonly string _memorySaveFile = Path.Combine(Path.GetTempPath(), "MemorySaveFile.bin");
        private readonly string _registersSaveFile = Path.Combine(Path.GetTempPath(), "RegistersSaveFile.bin");
        private readonly string _machineSaveFile = Path.Combine(Path.GetTempPath(), "MachineSaveFile.bin");

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            DeleteSaveFiles();
        }

        [Test]
        public void ShouldBeAbleToSerializeMemory()
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
        public void ShouldBeAbleToSerializeRegisters()
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
        public void ShouldBeAbleToSerializeMachine()
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
            File.Delete(_memorySaveFile);
            File.Delete(_registersSaveFile);
            File.Delete(_machineSaveFile);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DeleteSaveFiles();
        }
    }
}
