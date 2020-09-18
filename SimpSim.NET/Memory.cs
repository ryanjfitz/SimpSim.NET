using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SimpSim.NET
{
    public class Memory : IEnumerable<byte>
    {
        public event Action Changed;

        private byte[] _bytes;

        public Memory()
        {
            LoadByteArray(new byte[0x100]);
        }

        public byte this[byte address]
        {
            get => _bytes[address];
            set
            {
                byte newValue = value;
                byte oldValue = _bytes[address];

                if (newValue != oldValue)
                {
                    _bytes[address] = newValue;
                    Changed?.Invoke();
                }
            }
        }

        public void LoadByteArray(IEnumerable<byte> bytes)
        {
            byte[] byteArray = bytes.ToArray();

            if (byteArray.Length != 0x100)
                throw new ArgumentException("Array must have a length of 0x100.", nameof(bytes));

            _bytes = byteArray;
            Changed?.Invoke();
        }

        public void LoadInstructions(Instruction[] instructions)
        {
            byte address = 0x00;
            foreach (Instruction instruction in instructions)
            {
                LoadInstruction(instruction, address);
                address += (byte)instruction.Bytes.Count;
            }
        }

        public void LoadInstruction(Instruction instruction, byte address)
        {
            this[address] = instruction.Byte1;
            this[++address] = instruction.Byte2;
        }

        public Instruction GetInstruction(byte address)
        {
            return new Instruction(this[address], this[++address]);
        }

        public void Clear()
        {
            Array.Clear(_bytes, 0, _bytes.Length);
            Changed?.Invoke();
        }

        public IEnumerator<byte> GetEnumerator()
        {
            return ((IEnumerable<byte>)_bytes).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}