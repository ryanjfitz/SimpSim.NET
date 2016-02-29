using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SimpSim.NET.Tests
{
    [TestFixture]
    public class MachineTests
    {
        private Memory _memory;
        private Registers _registers;
        private Machine _machine;

        [SetUp]
        public void SetUp()
        {
            _memory = new Memory();
            _registers = new Registers();
            _machine = new Machine(_memory, _registers);
        }

        [Test]
        public void ProgramCounterShouldIncrementAfterStep()
        {
            _machine.ProgramCounter = 0x00;

            for (int i = 0; i <= byte.MaxValue; i++)
            {
                byte expected = (byte)(_machine.ProgramCounter + 0x02);
                _machine.Step();
                Assert.AreEqual(expected, _machine.ProgramCounter);
            }
        }

        [Test]
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

            Assert.AreEqual(instruction3, _machine.InstructionRegister);
        }

        [Test]
        public void StateShouldChangeToRunningWhileRunning()
        {
            LaunchNonTerminatingProgram();

            Assert.AreEqual(Machine.MachineState.Running, _machine.State);
        }

        [Test]
        public void ShouldBeAbleToStopRunningProgram()
        {
            Task task = LaunchNonTerminatingProgram();

            Assert.AreEqual(Machine.MachineState.Running, _machine.State);

            _machine.Break();

            // Wait for pending machine steps to execute.
            task.Wait();

            Assert.AreEqual(Machine.MachineState.Ready, _machine.State);
        }

        private Task LaunchNonTerminatingProgram()
        {
            // Load a program that runs forever.
            _memory.LoadInstructions(SamplePrograms.OutputTestInstructions);

            Task task = Task.Run(() => _machine.Run());

            while (task.Status != TaskStatus.Running)
            {
                // Wait until the task is runnning.
            }

            // Give the program time to begin executing.
            Thread.Sleep(100);

            return task;
        }
    }
}
