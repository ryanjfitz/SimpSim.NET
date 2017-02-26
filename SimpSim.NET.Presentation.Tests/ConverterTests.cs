using NUnit.Framework;

namespace SimpSim.NET.Presentation.Tests
{
    [TestFixture]
    public class ConverterTests
    {
        [Test]
        public void InstructionToStringConverter_WhenConvertingInstructionObject_ReturnsHexString()
        {
            InstructionToStringConverter converter = new InstructionToStringConverter();

            Assert.AreEqual("", converter.Convert(null, null, null, null));

            Assert.AreEqual("10FF", converter.Convert(new Instruction(0x10, 0xFF), null, null, null));

            Assert.AreEqual("AA7E", converter.Convert(new Instruction(0xAA, 0x7E), null, null, null));

            Assert.AreEqual("00FF", converter.Convert(new Instruction(0x00, 0xFF), null, null, null));
        }

        [Test]
        public void DecimalToHexConverter_WhenConvertingNumber_ReturnsHexString()
        {
            DecimalToHexConverter converter = new DecimalToHexConverter();

            Assert.AreEqual("03", converter.Convert(3, null, null, null));

            Assert.AreEqual("0D", converter.Convert(13, null, null, null));

            Assert.AreEqual("5F", converter.Convert(95, null, null, null));
        }

        [Test]
        public void DecimalToHexConverter_WhenConvertingBackFromHexString_ReturnsNumber()
        {
            DecimalToHexConverter converter = new DecimalToHexConverter();

            Assert.AreEqual(11, converter.ConvertBack("0B", null, null, null));

            Assert.AreEqual(27, converter.ConvertBack("1B", null, null, null));

            Assert.AreEqual(83, converter.ConvertBack("53", null, null, null));
        }
    }
}
