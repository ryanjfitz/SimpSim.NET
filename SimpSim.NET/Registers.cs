using System;
using System.Collections.Specialized;

namespace SimpSim.NET
{
    [Serializable]
    public class Registers : INotifyCollectionChanged
    {
        [field: NonSerialized]
        public event Action<char> ValueWrittenToOutputRegister;

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

                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem: newValue, oldItem: oldValue, index: register));
                }

                if (register == 0x0f)
                    ValueWrittenToOutputRegister?.Invoke((char)newValue);
            }
        }

        public void Clear()
        {
            for (byte b = 0; b < _array.Length; b++)
                this[b] = 0x00;
        }

        [field: NonSerialized]
        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}