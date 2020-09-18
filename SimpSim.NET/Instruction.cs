using System;
using System.Collections.Generic;

namespace SimpSim.NET
{
    public class Instruction : IEquatable<Instruction>
    {
        public Instruction(byte byte1, byte byte2)
        {
            Bytes = new[] { byte1, byte2 };
        }

        public IReadOnlyList<byte> Bytes { get; }

        public byte Byte1 => Bytes[0];

        public byte Byte2 => Bytes[1];

        public byte Nibble1 => Byte1.HighNibble();

        public byte Nibble2 => Byte1.LowNibble();

        public byte Nibble3 => Byte2.HighNibble();

        public byte Nibble4 => Byte2.LowNibble();

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
            return Byte1.ToHexString(2) + Byte2.ToHexString(2);
        }
    }
}