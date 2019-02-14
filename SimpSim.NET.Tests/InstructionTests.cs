using NUnit.Framework;

namespace SimpSim.NET.Tests
{
    [TestFixture]
    public class InstructionTests
    {

        [Test]
        public void ShouldBeAbleToGetBytes()
        {
            Instruction instruction = new Instruction(0x21, 0x10);

            CollectionAssert.AreEqual(new[] { 0x21, 0x10 }, instruction.Bytes);
        }

        [Test]
        public void ShouldBeAbleToGetByte1()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                Instruction instruction = new Instruction((byte)i, 0x00);
                Assert.AreEqual(i, instruction.Byte1);
            }
        }

        [Test]
        public void ShouldBeAbleToGetByte2()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                Instruction instruction = new Instruction(0x00, (byte)i);
                Assert.AreEqual(i, instruction.Byte2);
            }
        }

        [Test]
        public void ShouldBeAbleToGetNibble1()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                Instruction instruction = new Instruction((byte)i, 0x00);
                string hexNibble1 = i.ToString("X2")[0].ToString();
                byte expected = hexNibble1.ToByteFromHex();
                Assert.AreEqual(expected, instruction.Nibble1);
            }
        }

        [Test]
        public void ShouldBeAbleToGetNibble2()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                Instruction instruction = new Instruction((byte)i, 0x00);
                string hexNibble2 = i.ToString("X2")[1].ToString();
                byte expected = hexNibble2.ToByteFromHex();
                Assert.AreEqual(expected, instruction.Nibble2);
            }
        }

        [Test]
        public void ShouldBeAbleToGetNibble3()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                Instruction instruction = new Instruction(0x00, (byte)i);
                string hexNibble3 = i.ToString("X2")[0].ToString();
                byte expected = hexNibble3.ToByteFromHex();
                Assert.AreEqual(expected, instruction.Nibble3);
            }
        }

        [Test]
        public void ShouldBeAbleToGetNibble4()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                Instruction instruction = new Instruction(0x00, (byte)i);
                string hexNibble4 = i.ToString("X2")[1].ToString();
                byte expected = hexNibble4.ToByteFromHex();
                Assert.AreEqual(expected, instruction.Nibble4);
            }
        }

        [Test]
        public void InstructionObjectsWithEqualBytesShouldBeEqual()
        {
            Instruction instruction1 = new Instruction(0x77, 0x89);
            Instruction instruction2 = new Instruction(0x77, 0x89);

            Assert.That(instruction1.Equals(instruction2));
            Assert.That(instruction2.Equals(instruction1));
        }

        [Test]
        public void InstructionObjectsWithEqualBytesShouldHaveEqualHashCodes()
        {
            Instruction instruction1 = new Instruction(0x77, 0x89);
            Instruction instruction2 = new Instruction(0x77, 0x89);

            Assert.That(instruction1.GetHashCode() == instruction2.GetHashCode());
        }
    }
}
