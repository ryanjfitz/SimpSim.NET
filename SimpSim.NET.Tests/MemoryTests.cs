using Xunit;

namespace SimpSim.NET.Tests
{
    public class MemoryTests
    {
        private readonly Memory _memory;

        public MemoryTests()
        {
            _memory = new Memory();
        }

        [Fact]
        public void ShouldBeAbleToWriteToAllMemoryBytes()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
                _memory[(byte)i] = 0xFF;

            for (int i = 0; i <= byte.MaxValue; i++)
                Assert.Equal(0xFF, _memory[(byte)i]);
        }

        [Fact]
        public void ShouldWriteInstructionToMemoryAddress()
        {
            Instruction instruction = new Instruction(0x03, 0x04);

            byte address = 0x06;

            _memory.LoadInstruction(instruction, address);

            Assert.Equal(instruction.Byte1, _memory[address]);
            Assert.Equal(instruction.Byte2, _memory[++address]);
        }

        [Fact]
        public void ShouldWriteInstructionsToMemory()
        {
            Instruction[] instructions =
            {
                new Instruction(0x21, 0x10),
                new Instruction(0x22, 0x01)
            };

            _memory.LoadInstructions(instructions);

            Assert.Equal(0x21, _memory[0x00]);
            Assert.Equal(0x10, _memory[0x01]);
            Assert.Equal(0x22, _memory[0x02]);
            Assert.Equal(0x01, _memory[0x03]);
        }

        [Fact]
        public void ShouldBeAbleToRetrieveInstructionAtAddress()
        {
            Instruction instruction = new Instruction(0xAA, 0xBB);

            const byte address = 0xFF;

            _memory.LoadInstruction(instruction, address);

            Instruction actual = _memory.GetInstruction(address);

            Assert.Equal(instruction, actual);
        }

        [Fact]
        public void ShouldBeAbleToClearMemory()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
                _memory[(byte)i] = 0xFF;

            _memory.Clear();

            for (int i = 0; i <= byte.MaxValue; i++)
                Assert.Equal(0x00, _memory[(byte)i]);
        }
    }
}
