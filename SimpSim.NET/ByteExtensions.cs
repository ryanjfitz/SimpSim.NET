using System;
using System.Globalization;

namespace SimpSim.NET
{
    public static class ByteExtensions
    {
        public static byte ToByteFromHex(this string hexString)
        {
            return byte.Parse(hexString, NumberStyles.HexNumber);
        }

        public static byte Combine(this (object highNibble, object lowNibble) nibbles)
        {
            byte h = Convert.ToByte(nibbles.highNibble);
            byte l = Convert.ToByte(nibbles.lowNibble);

            int result = ((h << 4) & 0xF0) | (l & 0x0F);

            return (byte)result;
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
