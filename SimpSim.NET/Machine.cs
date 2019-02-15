using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SimpSim.NET
{
    [Serializable]
    public class Machine : INotifyPropertyChanged
    {
        private readonly Memory _memory;
        private readonly Registers _registers;
        private bool _breakPending;
        private byte _programCounter;
        private Instruction _instructionRegister;
        private MachineState _state;

        public Machine(Memory memory, Registers registers)
        {
            _memory = memory;
            _registers = registers;
            State = MachineState.Ready;
        }

        public byte ProgramCounter
        {
            get => _programCounter;
            set
            {
                if (_programCounter != value)
                {
                    _programCounter = value;
                    OnPropertyChanged();
                }
            }
        }

        public Instruction InstructionRegister
        {
            get => _instructionRegister;
            private set
            {
                if (!Equals(_instructionRegister, value))
                {
                    _instructionRegister = value;
                    OnPropertyChanged();
                }
            }
        }

        public MachineState State
        {
            get => _state;
            private set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged();
                }
            }
        }

        public void Run(int millisecondsBetweenSteps = 0)
        {
            if (State == MachineState.Running)
                throw new InvalidOperationException("A run operation may not be performed while the machine is already running.");

            State = MachineState.Running;

            while (State == MachineState.Running)
                if (_breakPending)
                {
                    State = MachineState.Ready;
                    _breakPending = false;
                }
                else
                {
                    Step();
                    Thread.Sleep(millisecondsBetweenSteps);
                }
        }

        public void Step()
        {
            Instruction instruction = _memory.GetInstruction(ProgramCounter);

            ExecuteInstruction(instruction);

            InstructionRegister = instruction;
        }

        public void Break()
        {
            if (State != MachineState.Running)
                throw new InvalidOperationException("A break operation may only be performed while the machine is running.");

            _breakPending = true;
        }

        private void ExecuteInstruction(Instruction instruction)
        {
            bool incrementProgramCounter = true;

            MachineState newState = State;

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
                    _registers[instruction.Nibble2] = FloatingPointAdd(_registers[instruction.Nibble3], _registers[instruction.Nibble4]);
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

        /// <remarks>Reproduced with permission. Refer to CREDITS text file in project root directory.</remarks>
        private byte FloatingPointAdd(byte operand1, byte operand2)
        {
            int[] sign = new int[2];
            sign[0] = 1;
            sign[1] = -1;

            int sign1 = operand1 / 128;
            int sign2 = operand2 / 128;
            int exponent1 = (operand1 & 0x70) / 16;
            int exponent2 = (operand2 & 0x70) / 16;
            int mantissa1 = (operand1 % 16 << exponent1) * sign[sign1];
            int mantissa2 = (operand2 % 16 << exponent2) * sign[sign2];

            int resultSign = 0;
            int resultMantissa = mantissa1 + mantissa2;

            if (resultMantissa < 0)
            {
                resultSign = 0x8;
                resultMantissa = -resultMantissa;
            }

            int resultExponent = 0;

            while (resultMantissa > 15)
            {
                resultMantissa = resultMantissa / 2;
                resultExponent++;
            }

            int result = (resultSign | resultExponent) * 16 + resultMantissa;

            return (byte)result;
        }

        private void IncrementProgramCounter(Instruction instruction)
        {
            ProgramCounter += (byte)instruction.Bytes.Count;
        }

        public enum MachineState
        {
            Halted,
            InvalidInstruction,
            Ready,
            Running
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}