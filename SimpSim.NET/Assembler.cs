using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpSim.NET
{
    public class Assembler
    {
        [ThreadStatic]
        private static IDictionary<string, byte> _symbolTable;
        private readonly InstructionByteCollection _instructionBytes;

        public Assembler()
        {
            _symbolTable = new Dictionary<string, byte>();
            _instructionBytes = new InstructionByteCollection();
        }

        public Instruction[] Assemble(string assemblyCode)
        {
            _symbolTable.Clear();
            _instructionBytes.Reset();

            foreach (string line in assemblyCode.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                InstructionSyntax instructionSyntax = InstructionSyntax.Parse(line);

                if (!string.IsNullOrWhiteSpace(instructionSyntax.Label))
                    AddLabelToSymbolTable(instructionSyntax.Label);

                if (!string.IsNullOrWhiteSpace(instructionSyntax.Mnemonic))
                    AssembleLine(instructionSyntax);
            }

            Instruction[] instructions = _instructionBytes.GetInstructionsFromBytes();

            return instructions;
        }

        private void AddLabelToSymbolTable(string label)
        {
            _symbolTable[label] = (byte)_instructionBytes.Count;
        }

        private void AssembleLine(InstructionSyntax instructionSyntax)
        {
            switch (instructionSyntax.Mnemonic.ToLowerInvariant())
            {
                case "load":
                    Load(instructionSyntax.Operands);
                    break;
                case "store":
                    Store(instructionSyntax.Operands);
                    break;
                case "move":
                    Move(instructionSyntax.Operands);
                    break;
                case "addi":
                    Addi(instructionSyntax.Operands);
                    break;
                case "addf":
                    Addf(instructionSyntax.Operands);
                    break;
                case "jmpeq":
                    JmpEQ(instructionSyntax.Operands);
                    break;
                case "jmple":
                    JmpLE(instructionSyntax.Operands);
                    break;
                case "jmp":
                    Jmp(instructionSyntax.Operands);
                    break;
                case "and":
                    And(instructionSyntax.Operands);
                    break;
                case "or":
                    Or(instructionSyntax.Operands);
                    break;
                case "xor":
                    Xor(instructionSyntax.Operands);
                    break;
                case "ror":
                    Ror(instructionSyntax.Operands);
                    break;
                case "db":
                    DataByte(instructionSyntax.Operands);
                    break;
                case "org":
                    Org(instructionSyntax.Operands);
                    break;
                case "halt":
                    Halt();
                    break;
                default:
                    throw new UnrecognizedMnemonicException();
            }
        }

        private void Load(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register) && AddressSyntax.TryParse(operands[1], out var address))
            {
                _instructionBytes.Add((Opcode.ImmediateLoad, register.Index).Combine());
                _instructionBytes.Add(address);
            }
            else if (RegisterSyntax.TryParse(operands[0], out register) && AddressSyntax.TryParse(operands[1], out address, BracketExpectation.Present))
            {
                _instructionBytes.Add((Opcode.DirectLoad, register.Index).Combine());
                _instructionBytes.Add(address);
            }
            else if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2, BracketExpectation.Present))
            {
                _instructionBytes.Add((Opcode.IndirectLoad, 0x0).Combine());
                _instructionBytes.Add((register1.Index, register2.Index).Combine());
            }
        }

        private void Store(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register) && AddressSyntax.TryParse(operands[1], out var address, BracketExpectation.Present))
            {
                _instructionBytes.Add((Opcode.DirectStore, register.Index).Combine());
                _instructionBytes.Add(address);
            }
            else if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2, BracketExpectation.Present))
            {
                _instructionBytes.Add((Opcode.IndirectStore, 0x0).Combine());
                _instructionBytes.Add((register1.Index, register2.Index).Combine());
            }
        }

        private void Move(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2))
            {
                _instructionBytes.Add((Opcode.Move, 0x0).Combine());
                _instructionBytes.Add((register1.Index, register2.Index).Combine());
            }
        }

        private void Addi(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
            {
                _instructionBytes.Add((Opcode.IntegerAdd, register1.Index).Combine());
                _instructionBytes.Add((register2.Index, register3.Index).Combine());
            }
        }

        private void Addf(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
            {
                _instructionBytes.Add((Opcode.FloatingPointAdd, register1.Index).Combine());
                _instructionBytes.Add((register2.Index, register3.Index).Combine());
            }
        }

        private void JmpEQ(string[] operands)
        {
            string[] registers = operands[0].Split('=');

            if (!RegisterSyntax.TryParse(registers[1], out var rightRegister) || rightRegister.Index != 0)
                throw new AssemblyException("Expected a comparison with R0.");

            if (RegisterSyntax.TryParse(registers[0], out var leftRegister) && AddressSyntax.TryParse(operands[1], out var address))
            {
                _instructionBytes.Add((Opcode.JumpEqual, leftRegister.Index).Combine());
                _instructionBytes.Add(address);
            }
        }

        private void JmpLE(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0].Split('<', '=')[0], out var register) && AddressSyntax.TryParse(operands[1], out var address))
            {
                _instructionBytes.Add((Opcode.JumpLessEqual, register.Index).Combine());
                _instructionBytes.Add(address);
            }
        }

        private void Jmp(string[] operands)
        {
            bool invalidSyntax = false;

            if (operands.Length != 1)
                invalidSyntax = true;
            else if (AddressSyntax.TryParse(operands[0], out var address))
            {
                _instructionBytes.Add((Opcode.JumpEqual, 0x0).Combine());
                _instructionBytes.Add(address);
            }
            else
                invalidSyntax = true;

            if (invalidSyntax)
                throw new AssemblyException("Expected a single address.");
        }

        private void And(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
            {
                _instructionBytes.Add((Opcode.And, register1.Index).Combine());
                _instructionBytes.Add((register2.Index, register3.Index).Combine());
            }
        }

        private void Or(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
            {
                _instructionBytes.Add((Opcode.Or, register1.Index).Combine());
                _instructionBytes.Add((register2.Index, register3.Index).Combine());
            }
        }

        private void Xor(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
            {
                _instructionBytes.Add((Opcode.Xor, register1.Index).Combine());
                _instructionBytes.Add((register2.Index, register3.Index).Combine());
            }
        }

        private void Ror(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register) && NumberSyntax.TryParse(operands[1], out byte number))
                if (number < 16)
                {
                    _instructionBytes.Add((Opcode.Ror, register.Index).Combine());
                    _instructionBytes.Add((0x0, number).Combine());
                }
                else
                    throw new AssemblyException("Number cannot be larger than 15.");
        }

        private void DataByte(string[] operands)
        {
            bool invalidSyntax = false;

            if (operands.Length == 0)
                invalidSyntax = true;
            else
                foreach (string operand in operands)
                    if (NumberSyntax.TryParse(operand, out byte number))
                        _instructionBytes.Add(number);
                    else if (StringLiteralSyntax.TryParse(operand, out string stringLiteral))
                        foreach (char c in stringLiteral)
                            _instructionBytes.Add((byte)c);
                    else
                    {
                        invalidSyntax = true;
                        break;
                    }

            if (invalidSyntax)
                throw new AssemblyException("Expected a number or string literal.");
        }

        private void Org(string[] operands)
        {
            bool invalidSyntax = false;

            if (operands.Length != 1)
                invalidSyntax = true;
            else if (NumberSyntax.TryParse(operands[0], out byte number))
                _instructionBytes.OriginAddress = number;
            else
                invalidSyntax = true;

            if (invalidSyntax)
                throw new AssemblyException("Expected a single number.");
        }

        private void Halt()
        {
            _instructionBytes.Add((Opcode.Halt, 0x0).Combine());
            _instructionBytes.Add(0x00);
        }

        private class InstructionSyntax
        {
            public string Comment { get; }
            public string Label { get; }
            public string Mnemonic { get; }
            public string[] Operands { get; }

            private const char CommentDelimiter = ';';
            private const char LabelDelimiter = ':';

            private InstructionSyntax(string comment, string label, string mnemonic, string[] operands)
            {
                Comment = comment;
                Label = label;
                Mnemonic = mnemonic;
                Operands = operands;
            }

            public static InstructionSyntax Parse(string line)
            {
                string comment = GetComment(ref line);

                string label = GetLabel(ref line);

                string mnemonic = GetMnemonic(line);

                string[] operands = GetOperands(line);

                return new InstructionSyntax(comment, label, mnemonic, operands);
            }

            private static string GetComment(ref string line)
            {
                string comment = "";

                string[] split = line.Split(new[] { CommentDelimiter }, 2);

                if (split.Length == 2)
                {
                    line = split[0];
                    comment = split[1].Trim();
                }

                return comment;
            }

            private static string GetLabel(ref string line)
            {
                string label = "";

                int delimiterIndex = line.IndexOf(LabelDelimiter);

                if (delimiterIndex > -1)
                {
                    label = line.Substring(0, delimiterIndex + 1).Trim();
                    line = line.Substring(delimiterIndex + 1);

                    if (!IsValidLabel(label))
                        throw new LabelAssemblyException(label);

                    label = label.TrimEnd(LabelDelimiter);
                }

                return label;
            }

            private static bool IsValidLabel(string input)
            {
                if (input.Length == 1 && input[0] == LabelDelimiter)
                    return false;

                if (input.Last() != LabelDelimiter)
                    return false;

                foreach (char c in input.TrimEnd(LabelDelimiter))
                    if (!"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789#_~".Contains(c))
                        return false;

                if (char.IsNumber(input[0]))
                    return false;

                return true;
            }

            private static string GetMnemonic(string line)
            {
                string[] split = line.Trim().Split();

                string mnemonic = split[0];

                return mnemonic;
            }

            private static string[] GetOperands(string line)
            {
                string[] split = line.Trim().Split(null, 2);

                string[] operands;

                if (split.Length == 2)
                    operands = split[1].Split(',').Select(o => o.Trim()).ToArray();
                else
                    operands = new string[] { };

                return operands;
            }

            public override string ToString()
            {
                return $"{Mnemonic} {string.Join(",", Operands)}";
            }
        }

        private class RegisterSyntax
        {
            private string Value { get; }

            private RegisterSyntax(string value)
            {
                Value = value;
            }

            public byte Index => Value[1].ToString().ToByteFromHex();

            public static bool TryParse(string input, out RegisterSyntax registerSyntax, BracketExpectation bracketExpectation = BracketExpectation.NotPresent)
            {
                bool isSuccess;

                bool isRegister = Regex.IsMatch(input, @"^\[?R[0-9A-F]\]?$");

                bool isSurroundedByBrackets = input.StartsWith("[") && input.EndsWith("]");

                if (bracketExpectation == BracketExpectation.Present)
                    isSuccess = isRegister && isSurroundedByBrackets;
                else
                    isSuccess = isRegister && !isSurroundedByBrackets;

                if (isSuccess)
                    registerSyntax = new RegisterSyntax(input.Trim('[', ']'));
                else
                    registerSyntax = null;

                return isSuccess;
            }

            public override string ToString()
            {
                return Value;
            }
        }

        private static class NumberSyntax
        {
            public static bool TryParse(string input, out byte number)
            {
                return TryParseDecimalLiteral(input, out number) || TryParseBinaryLiteral(input, out number) || TryParseHexLiteral(input, out number);
            }

            private static bool TryParseDecimalLiteral(string input, out byte number)
            {
                bool success = sbyte.TryParse(input.TrimEnd('d'), out sbyte signedNumber);

                if (signedNumber < 0)
                    number = (byte)signedNumber;
                else
                    success = byte.TryParse(input.TrimEnd('d'), out number);

                return success;
            }

            private static bool TryParseBinaryLiteral(string input, out byte number)
            {
                bool success;

                try
                {
                    number = Convert.ToByte(input.TrimEnd('b'), 2);
                    success = true;
                }
                catch
                {
                    number = 0;
                    success = false;
                }

                return success;
            }

            private static bool TryParseHexLiteral(string input, out byte number)
            {
                bool success = byte.TryParse(input.TrimStart("0x".ToCharArray()), NumberStyles.HexNumber, null, out number)
                               || byte.TryParse(input.TrimStart('$'), NumberStyles.HexNumber, null, out number)
                               || byte.TryParse(input.TrimEnd('h'), NumberStyles.HexNumber, null, out number) && !char.IsLetter(input.FirstOrDefault());

                if (!success)
                    number = 0;

                return success;
            }
        }

        private class AddressSyntax
        {
            public byte Value { get; }

            public string UndefinedLabel { get; }

            public bool ContainsUndefinedLabel => !string.IsNullOrWhiteSpace(UndefinedLabel);

            private AddressSyntax(byte value, string undefinedLabel)
            {
                Value = value;
                UndefinedLabel = undefinedLabel;
            }

            public static bool TryParse(string input, out AddressSyntax addressSyntax, BracketExpectation bracketExpectation = BracketExpectation.NotPresent)
            {
                bool isSuccess;

                bool isAddress = IsAddress(input.Trim('[', ']'), out byte address, out string undefinedLabel);

                bool isSurroundedByBrackets = input.StartsWith("[") && input.EndsWith("]");

                if (bracketExpectation == BracketExpectation.Present)
                    isSuccess = isAddress && isSurroundedByBrackets;
                else
                    isSuccess = isAddress && !isSurroundedByBrackets;

                if (isSuccess)
                    addressSyntax = new AddressSyntax(address, undefinedLabel);
                else
                    addressSyntax = null;

                return isSuccess;
            }

            private static bool IsAddress(string input, out byte address, out string undefinedLabel)
            {
                undefinedLabel = null;

                if (NumberSyntax.TryParse(input, out address))
                    return true;

                if (IsRegister(input))
                    return false;

                if (_symbolTable.ContainsKey(input))
                    address = _symbolTable[input];
                else
                    undefinedLabel = input;

                return true;
            }

            private static bool IsRegister(string input)
            {
                return RegisterSyntax.TryParse(input, out _);
            }

            public override string ToString()
            {
                return ContainsUndefinedLabel ? UndefinedLabel : Value.ToString();
            }
        }

        private static class StringLiteralSyntax
        {
            public static bool TryParse(string input, out string stringLiteral)
            {
                const char doubleQuote = '"';
                const char singleQuote = '\'';

                if (input.First() == doubleQuote && input.Last() == doubleQuote)
                {
                    stringLiteral = input.TrimStart(doubleQuote).TrimEnd(doubleQuote);
                    return true;
                }

                if (input.First() == singleQuote && input.Last() == singleQuote)
                {
                    stringLiteral = input.TrimStart(singleQuote).TrimEnd(singleQuote);
                    return true;
                }

                stringLiteral = null;
                return false;
            }
        }

        private class InstructionByteCollection
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

            public void Add(byte @byte)
            {
                _bytes[OriginAddress] = new InstructionByte(@byte);
                OriginAddress++;
            }

            public void Add(AddressSyntax addressSyntax)
            {
                _bytes[OriginAddress] = new InstructionByte(addressSyntax);
                OriginAddress++;
            }

            public void Reset()
            {
                Array.Clear(_bytes, 0, _bytes.Length);
                OriginAddress = 0;
                Count = 0;
            }

            public Instruction[] GetInstructionsFromBytes()
            {
                IList<Instruction> instructions = new List<Instruction>();

                for (int i = 0; i < Count; i += 2)
                {
                    byte byte1 = 0x00;
                    byte byte2 = 0x00;

                    if (_bytes[i] != null)
                        byte1 = _bytes[i].GetValue();

                    if (_bytes[i + 1] != null)
                        byte2 = _bytes[i + 1].GetValue();

                    instructions.Add(new Instruction(byte1, byte2));
                }

                return instructions.ToArray();
            }

            private class InstructionByte
            {
                private readonly byte _byte;
                private readonly AddressSyntax _address;
                private readonly AddressType _addressType;

                public InstructionByte(byte @byte)
                {
                    _byte = @byte;
                    _addressType = AddressType.DirectValue;
                }

                public InstructionByte(AddressSyntax address)
                {
                    _address = address;
                    if (address.ContainsUndefinedLabel)
                        _addressType = AddressType.Label;
                    else
                    {
                        _addressType = AddressType.DirectValue;
                        _byte = _address.Value;
                    }
                }

                public byte GetValue()
                {
                    switch (_addressType)
                    {
                        case AddressType.DirectValue:
                            return _byte;
                        case AddressType.Label:
                            if (!_symbolTable.ContainsKey(_address.UndefinedLabel))
                                throw new LabelAssemblyException(_address.UndefinedLabel);

                            return _symbolTable[_address.UndefinedLabel];
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

        private enum BracketExpectation
        {
            Present,
            NotPresent
        }
    }
}