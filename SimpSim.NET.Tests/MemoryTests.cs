using NUnit.Framework;

namespace SimpSim.NET.Tests
{
    [TestFixture]
    public class MemoryTests
    {
        private Memory _memory;

        [SetUp]
        public void SetUp()
        {
            _memory = new Memory();
        }

        [Test]
        public void ShouldBeAbleToWriteToAllMemoryBytes()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
                _memory[(byte)i] = 0xFF;

            for (int i = 0; i <= byte.MaxValue; i++)
                Assert.AreEqual(0xFF, _memory[(byte)i]);
        }

        [Test]
        public void ShouldWriteInstructionToMemoryAddress()
        {
            Instruction instruction = new Instruction(0x03, 0x04);

            byte address = 0x06;

            _memory.LoadInstruction(instruction, address);

            Assert.AreEqual(instruction.Byte1, _memory[address]);
            Assert.AreEqual(instruction.Byte2, _memory[++address]);
        }

        [Test]
        public void ShouldWriteInstructionsToMemory()
        {
            Instruction[] instructions =
            {
                new Instruction(0x21, 0x10),
                new Instruction(0x22, 0x01)
            };

            _memory.LoadInstructions(instructions);

            Assert.AreEqual(0x21, _memory[0x00]);
            Assert.AreEqual(0x10, _memory[0x01]);
            Assert.AreEqual(0x22, _memory[0x02]);
            Assert.AreEqual(0x01, _memory[0x03]);
        }

        [Test]
        public void ShouldBeAbleToRetrieveInstructionAtAddress()
        {
            Instruction instruction = new Instruction(0xAA, 0xBB);

            const byte address = 0xFF;

            _memory.LoadInstruction(instruction, address);

            Instruction actual = _memory.GetInstruction(address);

            Assert.AreEqual(instruction, actual);
        }
    }
}
