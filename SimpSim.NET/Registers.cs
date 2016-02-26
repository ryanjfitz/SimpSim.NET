namespace SimpSim.NET
{
    public class Registers
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
                _array[register] = value;

                if (register == 0x0f)
                    ValueWrittenToOutputRegister?.Invoke((char)value);
            }
        }
    }
}