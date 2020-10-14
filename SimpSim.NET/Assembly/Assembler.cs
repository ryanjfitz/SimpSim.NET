using System;
using System.Collections.Generic;

namespace SimpSim.NET
{
    public class Assembler
    {
        private readonly SymbolTable _symbolTable;
        private readonly InstructionByteCollection _bytes;
        private int _currentLineNumber;

        public Assembler()
        {
            _symbolTable = new SymbolTable();
            _bytes = new InstructionByteCollection();
        }

        public Instruction[] Assemble(string assemblyCode)
        {
            _symbolTable.Clear();
            _bytes.Clear();
            _currentLineNumber = 0;

            foreach (string line in GetLines(assemblyCode))
            {
                _currentLineNumber += 1;

                InstructionSyntax instructionSyntax = InstructionSyntax.Parse(line, _currentLineNumber);

                if (!string.IsNullOrWhiteSpace(instructionSyntax.Label))
                    AddLabelToSymbolTable(instructionSyntax.Label);

                if (!string.IsNullOrWhiteSpace(instructionSyntax.Mnemonic))
                    AssembleLine(instructionSyntax);
            }

            Instruction[] instructions = _bytes.ToInstructions(_symbolTable);

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
                    Halt(instructionSyntax.Operands);
                    break;
                default:
                    throw new UnrecognizedMnemonicException(_currentLineNumber);
            }
        }

        private void Load(string[] operands)
        {
            if (operands.Length == 2)
            {
                if (RegisterSyntax.TryParse(operands[0], out var register) && AddressSyntax.TryParse(operands[1], _symbolTable, out var address))
                {
                    _bytes.Add(((byte)Opcode.ImmediateLoad, register.Index).Combine(), _currentLineNumber);
                    _bytes.Add(address, _currentLineNumber);
                    return;
                }

                if (RegisterSyntax.TryParse(operands[0], out register) && AddressSyntax.TryParse(operands[1], _symbolTable, out address, BracketExpectation.Present))
                {
                    _bytes.Add(((byte)Opcode.DirectLoad, register.Index).Combine(), _currentLineNumber);
                    _bytes.Add(address, _currentLineNumber);
                    return;
                }

                if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2, BracketExpectation.Present))
                {
                    _bytes.Add(((byte)Opcode.IndirectLoad, (byte)0x0).Combine(), _currentLineNumber);
                    _bytes.Add((register1.Index, register2.Index).Combine(), _currentLineNumber);
                    return;
                }
            }

            throw new AssemblyException("Invalid operands for load instruction.", _currentLineNumber);
        }

        private void Store(string[] operands)
        {
            if (operands.Length == 2)
            {
                if (RegisterSyntax.TryParse(operands[0], out var register) && AddressSyntax.TryParse(operands[1], _symbolTable, out var address, BracketExpectation.Present))
                {
                    _bytes.Add(((byte)Opcode.DirectStore, register.Index).Combine(), _currentLineNumber);
                    _bytes.Add(address, _currentLineNumber);
                    return;
                }

                if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2, BracketExpectation.Present))
                {
                    _bytes.Add(((byte)Opcode.IndirectStore, (byte)0x0).Combine(), _currentLineNumber);
                    _bytes.Add((register1.Index, register2.Index).Combine(), _currentLineNumber);
                    return;
                }
            }

            throw new AssemblyException("Invalid operands for store instruction.", _currentLineNumber);
        }

        private void Move(string[] operands)
        {
            if (operands.Length == 2)
            {
                if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2))
                {
                    _bytes.Add(((byte)Opcode.Move, (byte)0x0).Combine(), _currentLineNumber);
                    _bytes.Add((register2.Index, register1.Index).Combine(), _currentLineNumber);
                    return;
                }
            }

            throw new AssemblyException("Invalid operands for move instruction.", _currentLineNumber);
        }

        private void Addi(string[] operands)
        {
            if (operands.Length == 3)
            {
                if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
                {
                    _bytes.Add(((byte)Opcode.IntegerAdd, register1.Index).Combine(), _currentLineNumber);
                    _bytes.Add((register2.Index, register3.Index).Combine(), _currentLineNumber);
                    return;
                }
            }

            throw new AssemblyException("Invalid operands for addi instruction.", _currentLineNumber);
        }

        private void Addf(string[] operands)
        {
            if (operands.Length == 3)
            {
                if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
                {
                    _bytes.Add(((byte)Opcode.FloatingPointAdd, register1.Index).Combine(), _currentLineNumber);
                    _bytes.Add((register2.Index, register3.Index).Combine(), _currentLineNumber);
                    return;
                }
            }

            throw new AssemblyException("Invalid operands for addf instruction.", _currentLineNumber);
        }

        private void JmpEQ(string[] operands)
        {
            if (operands.Length == 2)
            {
                string[] registers = operands[0].Split('=');

                if (!RegisterSyntax.TryParse(registers[1], out var rightRegister) || rightRegister.Index != 0)
                    throw new AssemblyException("Expected a comparison with R0.", _currentLineNumber);

                if (RegisterSyntax.TryParse(registers[0], out var leftRegister) && AddressSyntax.TryParse(operands[1], _symbolTable, out var address))
                {
                    _bytes.Add(((byte)Opcode.JumpEqual, leftRegister.Index).Combine(), _currentLineNumber);
                    _bytes.Add(address, _currentLineNumber);
                    return;
                }
            }

            throw new AssemblyException("Invalid operands for jmpeq instruction.", _currentLineNumber);
        }

        private void JmpLE(string[] operands)
        {
            if (operands.Length == 2)
            {
                if (RegisterSyntax.TryParse(operands[0].Split('<', '=')[0], out var register) && AddressSyntax.TryParse(operands[1], _symbolTable, out var address))
                {
                    _bytes.Add(((byte)Opcode.JumpLessEqual, register.Index).Combine(), _currentLineNumber);
                    _bytes.Add(address, _currentLineNumber);
                    return;
                }
            }

            throw new AssemblyException("Invalid operands for jmple instruction.", _currentLineNumber);
        }

        private void Jmp(string[] operands)
        {
            bool invalidSyntax = false;

            if (operands.Length != 1)
                invalidSyntax = true;
            else if (AddressSyntax.TryParse(operands[0], _symbolTable, out var address))
            {
                _bytes.Add(((byte)Opcode.JumpEqual, (byte)0x0).Combine(), _currentLineNumber);
                _bytes.Add(address, _currentLineNumber);
            }
            else
                invalidSyntax = true;

            if (invalidSyntax)
                throw new AssemblyException("Expected a single address.", _currentLineNumber);
        }

        private void And(string[] operands)
        {
            if (operands.Length == 3)
            {
                if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
                {
                    _bytes.Add(((byte)Opcode.And, register1.Index).Combine(), _currentLineNumber);
                    _bytes.Add((register2.Index, register3.Index).Combine(), _currentLineNumber);
                    return;
                }
            }

            throw new AssemblyException("Invalid operands for and instruction.", _currentLineNumber);
        }

        private void Or(string[] operands)
        {
            if (operands.Length == 3)
            {
                if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
                {
                    _bytes.Add(((byte)Opcode.Or, register1.Index).Combine(), _currentLineNumber);
                    _bytes.Add((register2.Index, register3.Index).Combine(), _currentLineNumber);
                    return;
                }
            }

            throw new AssemblyException("Invalid operands for or instruction.", _currentLineNumber);
        }

        private void Xor(string[] operands)
        {
            if (operands.Length == 3)
            {
                if (RegisterSyntax.TryParse(operands[0], out var register1) && RegisterSyntax.TryParse(operands[1], out var register2) && RegisterSyntax.TryParse(operands[2], out var register3))
                {
                    _bytes.Add(((byte)Opcode.Xor, register1.Index).Combine(), _currentLineNumber);
                    _bytes.Add((register2.Index, register3.Index).Combine(), _currentLineNumber);
                    return;
                }
            }

            throw new AssemblyException("Invalid operands for xor instruction.", _currentLineNumber);
        }

        private void Ror(string[] operands)
        {
            if (operands.Length == 2)
            {
                if (RegisterSyntax.TryParse(operands[0], out var register) && NumberSyntax.TryParse(operands[1], out byte number))
                    if (number < 16)
                    {
                        _bytes.Add(((byte)Opcode.Ror, register.Index).Combine(), _currentLineNumber);
                        _bytes.Add(((byte)0x0, number).Combine(), _currentLineNumber);
                        return;
                    }
                    else
                        throw new AssemblyException("Number cannot be larger than 15.", _currentLineNumber);
            }

            throw new AssemblyException("Invalid operands for ror instruction.", _currentLineNumber);
        }

        private void DataByte(string[] operands)
        {
            bool invalidSyntax = false;

            if (operands.Length == 0)
                invalidSyntax = true;
            else
                foreach (string operand in operands)
                    if (NumberSyntax.TryParse(operand, out byte number))
                        _bytes.Add(number, _currentLineNumber);
                    else if (StringLiteralSyntax.TryParse(operand, out string stringLiteral))
                        foreach (char c in stringLiteral)
                            _bytes.Add((byte)c, _currentLineNumber);
                    else
                    {
                        invalidSyntax = true;
                        break;
                    }

            if (invalidSyntax)
                throw new AssemblyException("Expected a number or string literal.", _currentLineNumber);
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
                throw new AssemblyException("Expected a single number.", _currentLineNumber);
        }

        private void Halt(string[] operands)
        {
            if (operands.Length > 0)
                throw new AssemblyException("Expected no operands for halt instruction.", _currentLineNumber);

            _bytes.Add(((byte)Opcode.Halt, (byte)0x0).Combine(), _currentLineNumber);
            _bytes.Add(0x00, _currentLineNumber);
        }
    }
}