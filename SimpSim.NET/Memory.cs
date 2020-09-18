using System;
using System.Linq;

namespace SimpSim.NET
{
    public class Memory
    {
        public event Action Changed;

        private readonly byte[] _array;

        public Memory() : this(new byte[0x100]) { }

        public Memory(byte[] array)
        {
            if (array.Length != 0x100)
                throw new ArgumentException("Array must have a length of 0x100.", nameof(array));

            _array = array;
        }

        public byte this[byte address]
        {
            get => _array[address];
            set
            {
                byte newValue = value;
                byte oldValue = _array[address];

                if (newValue != oldValue)
                {
                    _array[address] = newValue;

                    Changed?.Invoke();
                }
            }
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
            Array.Clear(_array, 0, _array.Length);
            Changed?.Invoke();
        }

        public byte[] ToArray()
        {
            return _array.ToArray();
        }
    }
}