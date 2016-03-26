using System;

namespace SimpSim.NET
{
    public class Instruction : IEquatable<Instruction>
    {
        public Instruction(byte byte1, byte byte2)
        {
            Bytes = new[] { byte1, byte2 };
        }

        public byte[] Bytes { get; }

        public byte Byte1 => Bytes[0];

        public byte Byte2 => Bytes[1];

        public byte Nibble1 => ByteUtilities.GetHighNibbleFromByte(Byte1);

        public byte Nibble2 => ByteUtilities.GetLowNibbleFromByte(Byte1);

        public byte Nibble3 => ByteUtilities.GetHighNibbleFromByte(Byte2);

        public byte Nibble4 => ByteUtilities.GetLowNibbleFromByte(Byte2);

        public bool Equals(Instruction other)
        {
            return Byte1 == other.Byte1 && Byte2 == other.Byte2;
        }

        public override string ToString()
        {
            return ByteUtilities.ConvertByteToHexString(Byte1, 2) + ByteUtilities.ConvertByteToHexString(Byte2, 2);
        }
    }
}