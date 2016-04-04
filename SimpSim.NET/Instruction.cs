using System;
using System.Collections.Generic;

namespace SimpSim.NET
{
    [Serializable]
    public class Instruction : IEquatable<Instruction>
    {
        public Instruction(byte byte1, byte byte2)
        {
            Bytes = new[] { byte1, byte2 };
        }

        public IReadOnlyList<byte> Bytes { get; }

        public byte Byte1 => Bytes[0];

        public byte Byte2 => Bytes[1];

        public byte Nibble1 => ByteUtilities.GetHighNibbleFromByte(Byte1);

        public byte Nibble2 => ByteUtilities.GetLowNibbleFromByte(Byte1);

        public byte Nibble3 => ByteUtilities.GetHighNibbleFromByte(Byte2);

        public byte Nibble4 => ByteUtilities.GetLowNibbleFromByte(Byte2);

        public bool Equals(Instruction other)
        {
            return other != null && Byte1 == other.Byte1 && Byte2 == other.Byte2;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Instruction);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;

                hashCode = 31 * hashCode + Byte1.GetHashCode();
                hashCode = 31 * hashCode + Byte2.GetHashCode();

                return hashCode;
            }
        }

        public override string ToString()
        {
            return ByteUtilities.ConvertByteToHexString(Byte1, 2) + ByteUtilities.ConvertByteToHexString(Byte2, 2);
        }
    }
}