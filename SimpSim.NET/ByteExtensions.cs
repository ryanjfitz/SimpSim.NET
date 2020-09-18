using System.Globalization;

namespace SimpSim.NET
{
    public static class ByteExtensions
    {
        public static byte ToByteFromHex(this string hexString)
        {
            return byte.Parse(hexString, NumberStyles.HexNumber);
        }

        public static byte Combine(this (byte highNibble, byte lowNibble) nibbles)
        {
            return (byte)(((nibbles.highNibble << 4) & 0xF0) | (nibbles.lowNibble & 0x0F));
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
