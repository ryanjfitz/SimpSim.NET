using System.Globalization;

namespace SimpSim.NET
{
    public static class ByteUtilities
    {
        public static byte ConvertHexStringToByte(string hexString)
        {
            return byte.Parse(hexString, NumberStyles.HexNumber);
        }

        public static byte GetByteFromNibbles(byte highNibble, byte lowNibble)
        {
            return (byte)(((highNibble << 4) & 0xF0) | (lowNibble & 0x0F));
        }

        public static string ToHexString(this byte b, int numberOfDigits = 1)
        {
            return b.ToString("X" + numberOfDigits);
        }

        public static byte HighNibble(this byte b)
        {
            return (byte)(b >> 4);
        }

        public static byte LowNibble(this byte b)
        {
            return (byte)(b & 0x0F);
        }
    }
}
