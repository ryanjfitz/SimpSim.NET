﻿namespace SimpSim.NET.Assembly;

internal class InstructionByteCollection
{
    private readonly InstructionByte[] _bytes;
    private byte _originAddress;

    public InstructionByteCollection()
    {
        _bytes = new InstructionByte[0x100];
    }

    public byte OriginAddress
    {
        private get
        {
            return _originAddress;
        }
        set
        {
            _originAddress = value;

            if (_originAddress > Count)
                Count = _originAddress;
        }
    }

    public int Count { get; private set; }

    public void Add(byte @byte, int lineNumber)
    {
        _bytes[OriginAddress] = new InstructionByte(@byte, lineNumber);
        OriginAddress++;
    }

    public void Add(AddressSyntax addressSyntax, int lineNumber)
    {
        _bytes[OriginAddress] = new InstructionByte(addressSyntax, lineNumber);
        OriginAddress++;
    }

    public void Clear()
    {
        Array.Clear(_bytes, 0, _bytes.Length);
        OriginAddress = 0;
        Count = 0;
    }

    public Instruction[] ToInstructions(SymbolTable symbolTable)
    {
        IList<Instruction> instructions = new List<Instruction>();

        for (int i = 0; i < Count; i += 2)
        {
            byte byte1 = 0x00;
            byte byte2 = 0x00;

            if (_bytes[i] != null)
                byte1 = _bytes[i].GetValue(symbolTable);

            if (_bytes[i + 1] != null)
                byte2 = _bytes[i + 1].GetValue(symbolTable);

            instructions.Add(new Instruction(byte1, byte2));
        }

        return instructions.ToArray();
    }
}