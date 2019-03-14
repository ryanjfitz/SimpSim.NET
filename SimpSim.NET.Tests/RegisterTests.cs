using Xunit;

namespace SimpSim.NET.Tests
{
    public class RegisterTests
    {
        private readonly Registers _registers;

        public RegisterTests()
        {
            _registers = new Registers();
        }

        [Fact]
        public void ShouldBeAbleToWriteToAllRegisters()
        {
            for (byte b = 0x00; b <= 0x0f; b++)
                _registers[b] = 0xff;

            for (byte b = 0x00; b <= 0x0f; b++)
                Assert.Equal(0xff, _registers[b]);
        }

        [Fact]
        public void ShouldRaiseEventContainingAsciiCharacterWhenOutputRegisterChanged()
        {
            string appendedOutput = null;

            _registers.ValueWrittenToOutputRegister += c => appendedOutput += c;

            _registers[0x0f] = 0x48;
            _registers[0x0f] = 0x65;
            _registers[0x0f] = 0x6C;
            _registers[0x0f] = 0x6C;
            _registers[0x0f] = 0x6F;
            _registers[0x0f] = 0x20;
            _registers[0x0f] = 0x77;
            _registers[0x0f] = 0x6F;
            _registers[0x0f] = 0x72;
            _registers[0x0f] = 0x6C;
            _registers[0x0f] = 0x64;
            _registers[0x0f] = 0x21;

            Assert.Equal("Hello world!", appendedOutput);
        }

        [Fact]
        public void ShouldBeAbleToClearRegisters()
        {
            for (byte b = 0; b <= 0x0f; b++)
                _registers[b] = 0xFF;

            _registers.Clear();

            for (byte b = 0; b <= 0x0f; b++)
                Assert.Equal(0x00, _registers[b]);
        }
    }
}
