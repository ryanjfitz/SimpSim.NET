using System.Collections.Generic;

namespace SimpSim.NET
{
    internal class InstructionByte
    {
        private readonly byte _byte;
        private readonly AddressSyntax _address;
        private readonly AddressType _addressType;
        private readonly int _lineNumber;

        public InstructionByte(byte @byte, int lineNumber)
        {
            _byte = @byte;
            _addressType = AddressType.DirectValue;
            _lineNumber = lineNumber;
        }

        public InstructionByte(AddressSyntax address, int lineNumber)
        {
            _address = address;
            if (address.ContainsUndefinedLabel)
                _addressType = AddressType.Label;
            else
            {
                _addressType = AddressType.DirectValue;
                _byte = _address.Value;
            }
            _lineNumber = lineNumber;
        }

        public byte GetValue(IDictionary<string, byte> symbolTable)
        {
            switch (_addressType)
            {
                case AddressType.DirectValue:
                    return _byte;
                case AddressType.Label:
                    if (!symbolTable.ContainsKey(_address.UndefinedLabel))
                        throw new LabelAssemblyException(_address.UndefinedLabel, _lineNumber);

                    return symbolTable[_address.UndefinedLabel];
                default:
                    return 0x00;
            }
        }

        private enum AddressType
        {
            DirectValue,
            Label
        }
    }
}