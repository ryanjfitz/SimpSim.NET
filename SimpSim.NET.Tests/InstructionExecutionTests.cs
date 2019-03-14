using System.Collections.Generic;
using Xunit;

namespace SimpSim.NET.Tests
{
    public class InstructionExecutionTests
    {
        private readonly Memory _memory;
        private readonly Registers _registers;
        private readonly Machine _machine;

        public InstructionExecutionTests()
        {
            _memory = new Memory();
            _registers = new Registers();
            _machine = new Machine(_memory, _registers);
        }

        [Fact]
        public void ShouldBeAbleToExecuteImmediateLoadInstruction()
        {
            Instruction immediateLoadInstruction = new Instruction(0x21, 0x10);

            _memory.LoadInstruction(immediateLoadInstruction, 0x00);

            _machine.Step();

            Assert.Equal(0x10, _registers[0x01]);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void ShouldBeAbleToExecuteDirectLoadInstruction()
        {
            Instruction directLoadInstruction = new Instruction(0x11, 0x20);

            _memory[0x20] = 0xDE;
            _memory.LoadInstruction(directLoadInstruction, 0x00);

            _machine.Step();

            Assert.Equal(0xDE, _registers[0x01]);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void ShouldBeAbleToExecuteIndirectLoadInstruction()
        {
            Instruction indirectLoadInstruction = new Instruction(0xD0, 0xF1);

            _registers[0x01] = 0x83;
            _memory[0x83] = 0xEE;
            _memory.LoadInstruction(indirectLoadInstruction, 0x00);

            _machine.Step();

            Assert.Equal(0xEE, _registers[0x0F]);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void ShouldBeAbleToExecuteIndirectStoreInstruction()
        {
            Instruction indirectStoreInstruction = new Instruction(0xE0, 0x4A);

            _registers[0x04] = 0x43;
            _registers[0x0A] = 0x72;

            _memory.LoadInstruction(indirectStoreInstruction, 0x00);

            _machine.Step();

            Assert.Equal(0x43, _memory[0x72]);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void ShouldBeAbleToExecuteMoveInstruction()
        {
            Instruction moveInstruction = new Instruction(0x40, 0xCD);

            _registers[0x0C] = 0x94;
            _registers[0x0D] = 0x4B;

            _memory.LoadInstruction(moveInstruction, 0x00);

            _machine.Step();

            Assert.Equal(0x94, _registers[0x0D]);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        public static IEnumerable<object[]> AddIntegerCases()
        {
            yield return new object[] { 0x09, 0x02, 0x0B };
            yield return new object[] { 0xC9, 0xC9, 0x92 };
            yield return new object[] { 0x0A, 0x0A, 0x14 };
            yield return new object[] { 0xFF, 0xFF, 0xFE };
        }

        [Theory]
        [MemberData(nameof(AddIntegerCases))]
        public void ShouldBeAbleToExecuteIntegerAddInstruction(byte operand1, byte operand2, byte result)
        {
            Instruction integerAddInstruction = new Instruction(0x51, 0x23);

            _registers[0x02] = operand1;
            _registers[0x03] = operand2;

            _memory.LoadInstruction(integerAddInstruction, 0x00);

            _machine.Step();

            Assert.Equal(result, _registers[0x01]);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        public static IEnumerable<object[]> AddFloatCases()
        {
            yield return new object[] { 0x09, 0x02, 0x0B };
            yield return new object[] { 0xC9, 0xC9, 0xD9 };
            yield return new object[] { 0x0A, 0x0A, 0x1A };
            yield return new object[] { 0xFF, 0xFF, 0x8F };
        }

        [Theory]
        [MemberData(nameof(AddFloatCases))]
        public void ShouldBeAbleToExecuteFloatingPointAddInstruction(byte operand1, byte operand2, byte result)
        {
            Instruction floatingPointAddInstruction = new Instruction(0x65, 0x67);

            _registers[0x06] = operand1;
            _registers[0x07] = operand2;

            _memory.LoadInstruction(floatingPointAddInstruction, 0x00);

            _machine.Step();

            Assert.Equal(result, _registers[0x05]);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void ShouldBeAbleToExecuteXorInstruction()
        {
            Instruction xorInstruction = new Instruction(0x91, 0x23);

            _registers[0x02] = 0xAA;
            _registers[0x03] = 0xFF;

            _memory.LoadInstruction(xorInstruction, 0x00);

            _machine.Step();

            Assert.Equal(0x55, _registers[0x01]);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void ShouldBeAbleToExecuteJumpEqualInstruction_HappyPath_EqualsR0()
        {
            Instruction jumpEqualInstruction = new Instruction(0xBF, 0xFF);

            _registers[0x0F] = 0x07;
            _registers[0x00] = 0x07;

            _memory.LoadInstruction(jumpEqualInstruction, 0x00);

            _machine.Step();

            Assert.Equal(0xFF, _machine.ProgramCounter);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void ShouldBeAbleToExecuteJumpEqualInstruction_SadPath_LessThanR0()
        {
            Instruction jumpEqualInstruction = new Instruction(0xBF, 0xFF);

            _registers[0x0F] = 0x06;
            _registers[0x00] = 0x07;

            _memory.LoadInstruction(jumpEqualInstruction, 0x00);

            _machine.Step();

            Assert.NotEqual(0xFF, _machine.ProgramCounter);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void ShouldBeAbleToExecuteJumpEqualInstruction_SadPath_GreaterThanR0()
        {
            Instruction jumpEqualInstruction = new Instruction(0xBF, 0xFF);

            _registers[0x0F] = 0x08;
            _registers[0x00] = 0x07;

            _memory.LoadInstruction(jumpEqualInstruction, 0x00);

            _machine.Step();

            Assert.NotEqual(0xFF, _machine.ProgramCounter);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void ShouldBeAbleToExecuteJumpLessEqualInstruction_HappyPath_EqualsR0()
        {
            Instruction jumpLessEqualInstruction = new Instruction(0xFF, 0xFF);

            _registers[0x0F] = 0x07;
            _registers[0x00] = 0x07;

            _memory.LoadInstruction(jumpLessEqualInstruction, 0x00);

            _machine.Step();

            Assert.Equal(0xFF, _machine.ProgramCounter);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void ShouldBeAbleToExecuteJumpLessEqualInstruction_HappyPath_LessThanR0()
        {
            Instruction jumpLessEqualInstruction = new Instruction(0xFF, 0xFF);

            _registers[0x0F] = 0x06;
            _registers[0x00] = 0x07;

            _memory.LoadInstruction(jumpLessEqualInstruction, 0x00);

            _machine.Step();

            Assert.Equal(0xFF, _machine.ProgramCounter);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void ShouldBeAbleToExecuteJumpLessEqualInstruction_SadPath_GreaterThanR0()
        {
            Instruction jumpLessEqualInstruction = new Instruction(0xFF, 0xFF);

            _registers[0x0F] = 0x08;
            _registers[0x00] = 0x07;

            _memory.LoadInstruction(jumpLessEqualInstruction, 0x00);

            _machine.Step();

            Assert.NotEqual(0xFF, _machine.ProgramCounter);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void ShouldBeAbleToExecuteDirectStoreInstruction()
        {
            Instruction directStoreInstruction = new Instruction(0x34, 0xEE);

            _registers[0x04] = 0x17;

            _memory.LoadInstruction(directStoreInstruction, 0x00);

            _machine.Step();

            Assert.Equal(0x17, _memory[0xEE]);
        }

        [Fact]
        public void ShouldBeAbleToExecuteOrInstruction()
        {
            Instruction orInstruction = new Instruction(0x7E, 0x47);

            for (int i = 0; i <= byte.MaxValue; i++)
                for (int j = 0; j <= byte.MaxValue; j++)
                {
                    _registers[0x04] = (byte)i;
                    _registers[0x07] = (byte)j;

                    _memory.LoadInstruction(orInstruction, 0x00);

                    _machine.ProgramCounter = 0x00;
                    _machine.Step();

                    Assert.Equal(i | j, _registers[0x0E]);
                    Assert.Equal(Machine.MachineState.Ready, _machine.State);
                }
        }

        [Fact]
        public void ShouldBeAbleToExecuteAndInstruction()
        {
            Instruction andInstruction = new Instruction(0x8E, 0x47);

            for (int i = 0; i <= byte.MaxValue; i++)
                for (int j = 0; j <= byte.MaxValue; j++)
                {
                    _registers[0x04] = (byte)i;
                    _registers[0x07] = (byte)j;

                    _memory.LoadInstruction(andInstruction, 0x00);

                    _machine.ProgramCounter = 0x00;
                    _machine.Step();

                    Assert.Equal(i & j, _registers[0x0E]);
                    Assert.Equal(Machine.MachineState.Ready, _machine.State);
                }
        }

        [Fact]
        public void ShouldBeAbleToExecuteRorInstruction()
        {
            Instruction rorInstruction = new Instruction(0xAD, 0x04);

            _registers[0x0D] = 0x30;

            _memory.LoadInstruction(rorInstruction, 0x00);

            _machine.Step();

            Assert.Equal(0x03, _registers[0x0D]);
            Assert.Equal(Machine.MachineState.Ready, _machine.State);
        }

        [Fact]
        public void HaltInstructionShouldChangeMachineStateToHalted()
        {
            Instruction haltInstruction = new Instruction(0xC0, 0x00);

            _memory.LoadInstruction(haltInstruction, 0x00);

            _machine.Step();

            Assert.Equal(Machine.MachineState.Halted, _machine.State);
        }

        [Fact]
        public void InvalidInstructionShouldChangeMachineStateToInvalidInstruction()
        {
            Instruction invalidInstruction = new Instruction(0x00, 0x00);

            _memory.LoadInstruction(invalidInstruction, 0x00);

            _machine.Step();

            Assert.Equal(Machine.MachineState.InvalidInstruction, _machine.State);
        }
    }
}