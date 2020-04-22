using System;

namespace SimpSim.NET
{
    [Serializable]
    public class Registers
    {
        [field: NonSerialized]
        public event Action<char> ValueWrittenToOutputRegister;
        [field: NonSerialized]
        public event Action Changed;

        private readonly byte[] _array;

        public Registers()
        {
            _array = new byte[16];
        }

        public byte this[byte register]
        {
            get => _array[register];
            set
            {
                byte newValue = value;
                byte oldValue = _array[register];

                if (newValue != oldValue)
                {
                    _array[register] = newValue;

                    Changed?.Invoke();
                }

                if (register == 0x0f)
                    ValueWrittenToOutputRegister?.Invoke((char)newValue);
            }
        }

        public void Clear()
        {
            Array.Clear(_array, 0, _array.Length);
            Changed?.Invoke();
        }
    }
}