using System.Collections.Generic;

namespace SimpSim.NET
{
    public class SymbolTable
    {
        private readonly IDictionary<string, byte> _dictionary;

        public SymbolTable()
        {
            _dictionary = new Dictionary<string, byte>();
        }

        public byte this[string label]
        {
            get => _dictionary[label];
            set => _dictionary[label] = value;
        }

        public bool ContainsLabel(string label)
        {
            return _dictionary.ContainsKey(label);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }
    }
}