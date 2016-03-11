using System.Collections.Specialized;

namespace SimpSim.NET
{
    public class Registers : INotifyCollectionChanged
    {
        public delegate void ValueWrittenToOutputRegisterHandler(char output);

        public event ValueWrittenToOutputRegisterHandler ValueWrittenToOutputRegister;

        private readonly byte[] _array;

        public Registers()
        {
            _array = new byte[16];
        }

        public byte this[byte register]
        {
            get
            {
                return _array[register];
            }
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

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}