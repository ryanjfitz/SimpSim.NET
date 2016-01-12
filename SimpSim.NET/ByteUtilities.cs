using System.Globalization;

namespace SimpSim.NET
{
    public static class ByteUtilities
    {
        public static byte ConvertHexStringToByte(string hexString)
        {
            return byte.Parse(hexString, NumberStyles.HexNumber);
        }

        public static string ConvertByteToHexString(byte b)
        {
            return b.ToString("X");
        }

        public static byte GetByteFromNibbles(byte highNibble, byte lowNibble)
        {
            return (byte)(((highNibble << 4) & 0xF0) | (lowNibble & 0x0F));
        }

        public static byte GetHighNibbleFromByte(byte b)
        {
            return (byte)(b >> 4);
        }

        public static byte GetLowNibbleFromByte(byte b)
        {
            return (byte)(b & 0x0F);
        }
    }
}
