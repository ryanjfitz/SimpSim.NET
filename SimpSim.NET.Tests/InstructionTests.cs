using Xunit;

namespace SimpSim.NET.Tests
{
    public class InstructionTests
    {
        [Fact]
        public void ShouldBeAbleToGetBytes()
        {
            Instruction instruction = new Instruction(0x21, 0x10);

            Assert.Equal(new byte[] { 0x21, 0x10 }, instruction.Bytes);
        }

        [Fact]
        public void ShouldBeAbleToGetByte1()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                Instruction instruction = new Instruction((byte)i, 0x00);
                Assert.Equal(i, instruction.Byte1);
            }
        }

        [Fact]
        public void ShouldBeAbleToGetByte2()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                Instruction instruction = new Instruction(0x00, (byte)i);
                Assert.Equal(i, instruction.Byte2);
            }
        }

        [Fact]
        public void ShouldBeAbleToGetNibble1()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                Instruction instruction = new Instruction((byte)i, 0x00);
                string hexNibble1 = i.ToString("X2")[0].ToString();
                byte expected = hexNibble1.ToByteFromHex();
                Assert.Equal(expected, instruction.Nibble1);
            }
        }

        [Fact]
        public void ShouldBeAbleToGetNibble2()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                Instruction instruction = new Instruction((byte)i, 0x00);
                string hexNibble2 = i.ToString("X2")[1].ToString();
                byte expected = hexNibble2.ToByteFromHex();
                Assert.Equal(expected, instruction.Nibble2);
            }
        }

        [Fact]
        public void ShouldBeAbleToGetNibble3()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                Instruction instruction = new Instruction(0x00, (byte)i);
                string hexNibble3 = i.ToString("X2")[0].ToString();
                byte expected = hexNibble3.ToByteFromHex();
                Assert.Equal(expected, instruction.Nibble3);
            }
        }

        [Fact]
        public void ShouldBeAbleToGetNibble4()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                Instruction instruction = new Instruction(0x00, (byte)i);
                string hexNibble4 = i.ToString("X2")[1].ToString();
                byte expected = hexNibble4.ToByteFromHex();
                Assert.Equal(expected, instruction.Nibble4);
            }
        }

        [Fact]
        public void InstructionObjectsWithEqualBytesShouldBeEqual()
        {
            Instruction instruction1 = new Instruction(0x77, 0x89);
            Instruction instruction2 = new Instruction(0x77, 0x89);

            Assert.True(instruction1.Equals(instruction2));
            Assert.True(instruction2.Equals(instruction1));
        }

        [Fact]
        public void InstructionObjectsWithEqualBytesShouldHaveEqualHashCodes()
        {
            Instruction instruction1 = new Instruction(0x77, 0x89);
            Instruction instruction2 = new Instruction(0x77, 0x89);

            Assert.True(instruction1.GetHashCode() == instruction2.GetHashCode());
        }
    }
}
