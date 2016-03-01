using System;
using System.Threading;

namespace SimpSim.NET
{
    public class Machine
    {
        private readonly Memory _memory;
        private readonly Registers _registers;
        private bool _running;
        private bool _breakPending;

        public Machine(Memory memory, Registers registers)
        {
            _memory = memory;
            _registers = registers;
            State = MachineState.Ready;
        }

        public byte ProgramCounter { get; set; }
        public Instruction InstructionRegister { get; private set; }
        public MachineState State { get; private set; }

        public void Run(int millisecondsBetweenSteps = 0)
        {
            _running = true;

            while (_running && State != MachineState.Halted && State != MachineState.InvalidInstruction)
            {
                if (_breakPending)
                {
                    State = MachineState.Ready;
                    _running = false;
                    _breakPending = false;
                }
                else
                {
                    Step();
                    Thread.Sleep(millisecondsBetweenSteps);
                }
            }

            _running = false;
        }

        public void Step()
        {
            Instruction instruction = _memory.GetInstruction(ProgramCounter);

            ExecuteInstruction(instruction);

            InstructionRegister = instruction;
        }

        public void Break()
        {
            if (!_running)
                throw new InvalidOperationException("A break operation may only be performed while the machine is running.");

            _breakPending = true;
        }

        private void ExecuteInstruction(Instruction instruction)
        {
            bool incrementProgramCounter = true;

            MachineState newState = _running ? MachineState.Running : MachineState.Ready;

            Opcode opcode = (Opcode)instruction.Nibble1;

            switch (opcode)
            {
                case Opcode.ImmediateLoad:
                    _registers[instruction.Nibble2] = instruction.Byte2;
                    break;
                case Opcode.DirectLoad:
                    _registers[instruction.Nibble2] = _memory[instruction.Byte2];
                    break;
                case Opcode.IndirectStore:
                    _memory[_registers[instruction.Nibble4]] = _registers[instruction.Nibble3];
                    break;
                case Opcode.Move:
                    _registers[instruction.Nibble4] = _registers[instruction.Nibble3];
                    break;
                case Opcode.IndirectLoad:
                    _registers[instruction.Nibble3] = _memory[_registers[instruction.Nibble4]];
                    break;
                case Opcode.IntegerAdd:
                    _registers[instruction.Nibble2] = (byte)(_registers[instruction.Nibble3] + _registers[instruction.Nibble4]);
                    break;
                case Opcode.JumpEqual:
                    if (_registers[instruction.Nibble2] == _registers[0x00])
                    {
                        ProgramCounter = instruction.Byte2;
                        incrementProgramCounter = false;
                    }
                    break;
                case Opcode.JumpLessEqual:
                    if (_registers[instruction.Nibble2] <= _registers[0x00])
                    {
                        ProgramCounter = instruction.Byte2;
                        incrementProgramCounter = false;
                    }
                    break;
                case Opcode.FloatingPointAdd:
                    // Floating point addition implementation reproduced with permission. Refer to CREDITS text file in project root directory.
                    int mant2 = _registers[instruction.Nibble3] % 16;
                    int mant3 = _registers[instruction.Nibble4] % 16;
                    int exp2 = (_registers[instruction.Nibble3] & 0x70) / 16;
                    int exp3 = (_registers[instruction.Nibble4] & 0x70) / 16;
                    int[] sign = new int[2];
                    sign[0] = 1;
                    sign[1] = -1;
                    int sign1 = 0;
                    int sign2 = _registers[instruction.Nibble3] / 128;
                    int sign3 = _registers[instruction.Nibble4] / 128;
                    mant2 = (mant2 << exp2) * sign[sign2];
                    mant3 = (mant3 << exp3) * sign[sign3];
                    int mant1 = mant2 + mant3;
                    if (mant1 < 0)
                    {
                        sign1 = 0x8;
                        mant1 = -mant1;
                    }
                    int exp1 = 0;
                    while (mant1 > 15)
                    {
                        mant1 = mant1 / 2;
                        exp1++;
                    }
                    _registers[instruction.Nibble2] = (byte)((sign1 | exp1) * 16 + mant1);
                    break;
                case Opcode.Or:
                    _registers[instruction.Nibble2] = (byte)(_registers[instruction.Nibble3] | _registers[instruction.Nibble4]);
                    break;
                case Opcode.And:
                    _registers[instruction.Nibble2] = (byte)(_registers[instruction.Nibble3] & _registers[instruction.Nibble4]);
                    break;
                case Opcode.Xor:
                    _registers[instruction.Nibble2] = (byte)(_registers[instruction.Nibble3] ^ _registers[instruction.Nibble4]);
                    break;
                case Opcode.Ror:
                    _registers[instruction.Nibble2] = (byte)((_registers[instruction.Nibble2] >> instruction.Nibble4) | (_registers[instruction.Nibble2] << (sizeof(byte) - instruction.Nibble4)));
                    break;
                case Opcode.DirectStore:
                    _memory[instruction.Byte2] = _registers[instruction.Nibble2];
                    break;
                case Opcode.Halt:
                    newState = MachineState.Halted;
                    break;
                default:
                    newState = MachineState.InvalidInstruction;
                    break;
            }

            State = newState;

            if (incrementProgramCounter)
                IncrementProgramCounter(instruction);
        }

        private void IncrementProgramCounter(Instruction instruction)
        {
            ProgramCounter += (byte)instruction.Bytes.Length;
        }

        public enum MachineState
        {
            Halted,
            InvalidInstruction,
            Ready,
            Running
        }
    }
}