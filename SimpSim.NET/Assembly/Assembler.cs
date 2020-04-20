using System;
using System.Collections.Generic;

namespace SimpSim.NET
{
    public class Assembler
    {
        private readonly IDictionary<string, byte> _symbolTable;
        private readonly InstructionByteCollection _bytes;
        private readonly AddressSyntaxParser _addressSyntaxParser;

        public Assembler()
        {
            _symbolTable = new Dictionary<string, byte>();
            _bytes = new InstructionByteCollection(_symbolTable);
            _addressSyntaxParser = new AddressSyntaxParser(_symbolTable);
        }

        public Instruction[] Assemble(string assemblyCode)
        {
            _symbolTable.Clear();
            _bytes.Clear();

            foreach (string line in GetLines(assemblyCode))
            {
                InstructionSyntax instructionSyntax = InstructionSyntax.Parse(line);

                if (!string.IsNullOrWhiteSpace(instructionSyntax.Label))
                    AddLabelToSymbolTable(instructionSyntax.Label);

                if (!string.IsNullOrWhiteSpace(instructionSyntax.Mnemonic))
                    AssembleLine(instructionSyntax);
            }

            Instruction[] instructions = _bytes.ToInstructions();

            return instructions;
        }

        private IEnumerable<string> GetLines(string assemblyCode)
        {
            return (assemblyCode ?? "").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }

        private void AddLabelToSymbolTable(string label)
        {
            _symbolTable[label] = (byte)_bytes.Count;
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
            if (RegisterSyntax.TryParse(operands[0], out var register) && _addressSyntaxParser.TryParse(operands[1], out var address))
            {
                _bytes.Add(Opcode.ImmediateLoad, register.Index);
                _bytes.Add(address);
            }
            else if (RegisterSyntax.TryParse(operands[0], out register) && _addressSyntaxParser.TryParse(operands[1], out address, BracketExpectation.Present))
            {
                _bytes.Add(Opcode.DirectLoad, register.Index);
                _bytes.Add(address);
            }
            else if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2, BracketExpectation.Present))
            {
                _bytes.Add(Opcode.IndirectLoad, 0x0);
                _bytes.Add(register1.Index, register2.Index);
            }
        }

        private void Store(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register) && _addressSyntaxParser.TryParse(operands[1], out var address, BracketExpectation.Present))
            {
                _bytes.Add(Opcode.DirectStore, register.Index);
                _bytes.Add(address);
            }
            else if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2, BracketExpectation.Present))
            {
                _bytes.Add(Opcode.IndirectStore, 0x0);
                _bytes.Add(register1.Index, register2.Index);
            }
        }

        private void Move(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2))
            {
                _bytes.Add(Opcode.Move, 0x0);
                _bytes.Add(register2.Index, register1.Index);
            }
        }

        private void Addi(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
            {
                _bytes.Add(Opcode.IntegerAdd, register1.Index);
                _bytes.Add(register2.Index, register3.Index);
            }
        }

        private void Addf(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
            {
                _bytes.Add(Opcode.FloatingPointAdd, register1.Index);
                _bytes.Add(register2.Index, register3.Index);
            }
        }

        private void JmpEQ(string[] operands)
        {
            string[] registers = operands[0].Split('=');

            if (!RegisterSyntax.TryParse(registers[1], out var rightRegister) || rightRegister.Index != 0)
                throw new AssemblyException("Expected a comparison with R0.");

            if (RegisterSyntax.TryParse(registers[0], out var leftRegister) && _addressSyntaxParser.TryParse(operands[1], out var address))
            {
                _bytes.Add(Opcode.JumpEqual, leftRegister.Index);
                _bytes.Add(address);
            }
        }

        private void JmpLE(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0].Split('<', '=')[0], out var register) && _addressSyntaxParser.TryParse(operands[1], out var address))
            {
                _bytes.Add(Opcode.JumpLessEqual, register.Index);
                _bytes.Add(address);
            }
        }

        private void Jmp(string[] operands)
        {
            bool invalidSyntax = false;

            if (operands.Length != 1)
                invalidSyntax = true;
            else if (_addressSyntaxParser.TryParse(operands[0], out var address))
            {
                _bytes.Add(Opcode.JumpEqual, 0x0);
                _bytes.Add(address);
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
                _bytes.Add(Opcode.And, register1.Index);
                _bytes.Add(register2.Index, register3.Index);
            }
        }

        private void Or(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
            {
                _bytes.Add(Opcode.Or, register1.Index);
                _bytes.Add(register2.Index, register3.Index);
            }
        }

        private void Xor(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
            {
                _bytes.Add(Opcode.Xor, register1.Index);
                _bytes.Add(register2.Index, register3.Index);
            }
        }

        private void Ror(string[] operands)
        {
            if (RegisterSyntax.TryParse(operands[0], out var register) && NumberSyntax.TryParse(operands[1], out byte number))
                if (number < 16)
                {
                    _bytes.Add(Opcode.Ror, register.Index);
                    _bytes.Add((byte)0x0, number);
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
                        _bytes.Add(number);
                    else if (StringLiteralSyntax.TryParse(operand, out string stringLiteral))
                        foreach (char c in stringLiteral)
                            _bytes.Add((byte)c);
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
                _bytes.OriginAddress = number;
            else
                invalidSyntax = true;

            if (invalidSyntax)
                throw new AssemblyException("Expected a single number.");
        }

        private void Halt()
        {
            _bytes.Add(Opcode.Halt, 0x0);
            _bytes.Add(0x00);
        }
    }
}