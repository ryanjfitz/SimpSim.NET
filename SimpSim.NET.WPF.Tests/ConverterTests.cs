using SimpSim.NET.WPF;
using Xunit;

namespace SimpSim.NET.Presentation.Tests
{
    public class ConverterTests
    {
        [Fact]
        public void DecimalToHexConverter_WhenConvertingNumber_ReturnsHexString()
        {
            DecimalToHexConverter converter = new DecimalToHexConverter();

            Assert.Equal("03", converter.Convert(3, null, null, null));

            Assert.Equal("0D", converter.Convert(13, null, null, null));

            Assert.Equal("5F", converter.Convert(95, null, null, null));
        }

        [Fact]
        public void DecimalToHexConverter_WhenConvertingBackFromHexString_ReturnsNumber()
        {
            DecimalToHexConverter converter = new DecimalToHexConverter();

            Assert.Equal((byte)0x0B, converter.ConvertBack("0B", null, null, null));

            Assert.Equal((byte)0x1B, converter.ConvertBack("1B", null, null, null));

            Assert.Equal((byte)0x53, converter.ConvertBack("53", null, null, null));
        }
    }
}
