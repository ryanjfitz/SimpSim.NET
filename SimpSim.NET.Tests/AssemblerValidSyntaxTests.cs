using Xunit;

namespace SimpSim.NET.Tests
{
    public class AssemblerValidSyntaxTests
    {
        private readonly Assembler _assembler;

        public AssemblerValidSyntaxTests()
        {
            _assembler = new Assembler();
        }

        [Fact]
        public void ShouldAssembleEmptyInstructions()
        {
            Assert.Empty(_assembler.Assemble(null));
        }

        [Fact]
        public void ShouldAssembleImmediateLoadInstructions()
        {
            for (int number = 0; number <= byte.MaxValue; number++)
            {
                byte b = (byte)number;

                Assert.Equal(new[] { new Instruction(0x20, b) }, _assembler.Assemble($"load R0,{b}"));
                Assert.Equal(new[] { new Instruction(0x21, b) }, _assembler.Assemble($"load R1,{b}"));
                Assert.Equal(new[] { new Instruction(0x22, b) }, _assembler.Assemble($"load R2,{b}"));
                Assert.Equal(new[] { new Instruction(0x23, b) }, _assembler.Assemble($"load R3,{b}"));
                Assert.Equal(new[] { new Instruction(0x24, b) }, _assembler.Assemble($"load R4,{b}"));
                Assert.Equal(new[] { new Instruction(0x25, b) }, _assembler.Assemble($"load R5,{b}"));
                Assert.Equal(new[] { new Instruction(0x26, b) }, _assembler.Assemble($"load R6,{b}"));
                Assert.Equal(new[] { new Instruction(0x27, b) }, _assembler.Assemble($"load R7,{b}"));
                Assert.Equal(new[] { new Instruction(0x28, b) }, _assembler.Assemble($"load R8,{b}"));
                Assert.Equal(new[] { new Instruction(0x29, b) }, _assembler.Assemble($"load R9,{b}"));
                Assert.Equal(new[] { new Instruction(0x2A, b) }, _assembler.Assemble($"load RA,{b}"));
                Assert.Equal(new[] { new Instruction(0x2B, b) }, _assembler.Assemble($"load RB,{b}"));
                Assert.Equal(new[] { new Instruction(0x2C, b) }, _assembler.Assemble($"load RC,{b}"));
                Assert.Equal(new[] { new Instruction(0x2D, b) }, _assembler.Assemble($"load RD,{b}"));
                Assert.Equal(new[] { new Instruction(0x2E, b) }, _assembler.Assemble($"load RE,{b}"));
                Assert.Equal(new[] { new Instruction(0x2F, b) }, _assembler.Assemble($"load RF,{b}"));
            }
        }

        [Fact]
        public void ShouldAssembleDirectLoadInstructions()
        {
            for (int number = 0; number <= byte.MaxValue; number++)
            {
                byte b = (byte)number;

                Assert.Equal(new[] { new Instruction(0x10, b) }, _assembler.Assemble($"load R0,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x11, b) }, _assembler.Assemble($"load R1,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x12, b) }, _assembler.Assemble($"load R2,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x13, b) }, _assembler.Assemble($"load R3,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x14, b) }, _assembler.Assemble($"load R4,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x15, b) }, _assembler.Assemble($"load R5,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x16, b) }, _assembler.Assemble($"load R6,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x17, b) }, _assembler.Assemble($"load R7,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x18, b) }, _assembler.Assemble($"load R8,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x19, b) }, _assembler.Assemble($"load R9,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x1A, b) }, _assembler.Assemble($"load RA,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x1B, b) }, _assembler.Assemble($"load RB,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x1C, b) }, _assembler.Assemble($"load RC,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x1D, b) }, _assembler.Assemble($"load RD,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x1E, b) }, _assembler.Assemble($"load RE,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x1F, b) }, _assembler.Assemble($"load RF,[{b}]"));
            }
        }

        [Fact]
        public void ShouldAssembleIndirectLoadInstructions()
        {
            for (byte registerIndex = 0; registerIndex < 16; registerIndex++)
            {
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0x0, registerIndex).Combine()) }, _assembler.Assemble($"load R0,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0x1, registerIndex).Combine()) }, _assembler.Assemble($"load R1,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0x2, registerIndex).Combine()) }, _assembler.Assemble($"load R2,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0x3, registerIndex).Combine()) }, _assembler.Assemble($"load R3,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0x4, registerIndex).Combine()) }, _assembler.Assemble($"load R4,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0x5, registerIndex).Combine()) }, _assembler.Assemble($"load R5,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0x6, registerIndex).Combine()) }, _assembler.Assemble($"load R6,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0x7, registerIndex).Combine()) }, _assembler.Assemble($"load R7,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0x8, registerIndex).Combine()) }, _assembler.Assemble($"load R8,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0x9, registerIndex).Combine()) }, _assembler.Assemble($"load R9,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0xA, registerIndex).Combine()) }, _assembler.Assemble($"load RA,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0xB, registerIndex).Combine()) }, _assembler.Assemble($"load RB,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0xC, registerIndex).Combine()) }, _assembler.Assemble($"load RC,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0xD, registerIndex).Combine()) }, _assembler.Assemble($"load RD,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0xE, registerIndex).Combine()) }, _assembler.Assemble($"load RE,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xD0, ((byte)0xF, registerIndex).Combine()) }, _assembler.Assemble($"load RF,[R{registerIndex.ToHexString()}]"));
            }
        }

        [Fact]
        public void ShouldAssembleAddIntegerInstructions()
        {
            for (byte i = 0; i < 16; i++)
                for (byte j = 0; j < 16; j++)
                    for (byte k = 0; k < 16; k++)
                    {
                        Instruction[] expected = { new Instruction(((byte)0x5, i).Combine(), (j, k).Combine()) };
                        Instruction[] actual = _assembler.Assemble($"addi R{i.ToHexString()},R{j.ToHexString()},R{k.ToHexString()}");

                        Assert.Equal(expected, actual);
                    }
        }

        [Fact]
        public void ShouldAssembleAddFloatInstructions()
        {
            for (byte i = 0; i < 16; i++)
                for (byte j = 0; j < 16; j++)
                    for (byte k = 0; k < 16; k++)
                    {
                        Instruction[] expected = { new Instruction(((byte)0x6, i).Combine(), (j, k).Combine()) };
                        Instruction[] actual = _assembler.Assemble($"addf R{i.ToHexString()},R{j.ToHexString()},R{k.ToHexString()}");

                        Assert.Equal(expected, actual);
                    }
        }

        [Fact]
        public void ShouldAssembleBitwiseAndInstructions()
        {
            for (byte i = 0; i < 16; i++)
                for (byte j = 0; j < 16; j++)
                    for (byte k = 0; k < 16; k++)
                    {
                        Instruction[] expected = { new Instruction(((byte)0x8, i).Combine(), (j, k).Combine()) };
                        Instruction[] actual = _assembler.Assemble($"and R{i.ToHexString()},R{j.ToHexString()},R{k.ToHexString()}");

                        Assert.Equal(expected, actual);
                    }
        }

        [Fact]
        public void ShouldAssembleBitwiseOrInstructions()
        {
            for (byte i = 0; i < 16; i++)
                for (byte j = 0; j < 16; j++)
                    for (byte k = 0; k < 16; k++)
                    {
                        Instruction[] expected = { new Instruction(((byte)0x7, i).Combine(), (j, k).Combine()) };
                        Instruction[] actual = _assembler.Assemble($"or R{i.ToHexString()},R{j.ToHexString()},R{k.ToHexString()}");

                        Assert.Equal(expected, actual);
                    }
        }

        [Fact]
        public void ShouldAssembleBitwiseExclusiveOrInstructions()
        {
            for (byte i = 0; i < 16; i++)
                for (byte j = 0; j < 16; j++)
                    for (byte k = 0; k < 16; k++)
                    {
                        Instruction[] expected = { new Instruction(((byte)0x9, i).Combine(), (j, k).Combine()) };
                        Instruction[] actual = _assembler.Assemble($"xor R{i.ToHexString()},R{j.ToHexString()},R{k.ToHexString()}");

                        Assert.Equal(expected, actual);
                    }
        }

        [Fact]
        public void ShouldAssembleRorInstructions()
        {
            for (byte number = 0; number < 16; number++)
            {
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0x0).Combine(), number) }, _assembler.Assemble($"ror R0,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0x1).Combine(), number) }, _assembler.Assemble($"ror R1,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0x2).Combine(), number) }, _assembler.Assemble($"ror R2,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0x3).Combine(), number) }, _assembler.Assemble($"ror R3,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0x4).Combine(), number) }, _assembler.Assemble($"ror R4,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0x5).Combine(), number) }, _assembler.Assemble($"ror R5,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0x6).Combine(), number) }, _assembler.Assemble($"ror R6,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0x7).Combine(), number) }, _assembler.Assemble($"ror R7,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0x8).Combine(), number) }, _assembler.Assemble($"ror R8,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0x9).Combine(), number) }, _assembler.Assemble($"ror R9,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0xA).Combine(), number) }, _assembler.Assemble($"ror RA,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0xB).Combine(), number) }, _assembler.Assemble($"ror RB,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0xC).Combine(), number) }, _assembler.Assemble($"ror RC,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0xD).Combine(), number) }, _assembler.Assemble($"ror RD,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0xE).Combine(), number) }, _assembler.Assemble($"ror RE,{number}"));
                Assert.Equal(new[] { new Instruction(((byte)0xA, (byte)0xF).Combine(), number) }, _assembler.Assemble($"ror RF,{number}"));
            }
        }

        [Fact]
        public void ShouldAssembleJumpEqualInstructions()
        {
            for (byte registerIndex = 0; registerIndex < 16; registerIndex++)
                for (int number = 0; number <= byte.MaxValue; number++)
                    Assert.Equal(new[] { new Instruction(((byte)0xB, registerIndex).Combine(), (byte)number) }, _assembler.Assemble($"jmpEQ R{registerIndex.ToHexString()}=R0,{number}"));
        }

        [Fact]
        public void ShouldAssembleJumpLessEqualInstructions()
        {
            for (byte registerIndex = 0; registerIndex < 16; registerIndex++)
                for (int number = 0; number <= byte.MaxValue; number++)
                    Assert.Equal(new[] { new Instruction(((byte)0xF, registerIndex).Combine(), (byte)number) }, _assembler.Assemble($"jmpLE R{registerIndex.ToHexString()}<=R0,{number}"));
        }

        [Fact]
        public void ShouldAssembleJumpInstructions()
        {
            for (int number = 0; number <= byte.MaxValue; number++)
                Assert.Equal(new[] { new Instruction(0xB0, (byte)number) }, _assembler.Assemble($"jmp {number}"));
        }

        [Fact]
        public void ShouldAssembleDataByteInstructions()
        {
            Instruction[] expected = {
                new Instruction(0x01, 0x04),
                new Instruction(0x09, 0x10),
                new Instruction(0x19, 0x24),
                new Instruction(0x48, 0x65),
                new Instruction(0x6C, 0x6C),
                new Instruction(0x6F, 0x20),
                new Instruction(0x57, 0x6F),
                new Instruction(0x72, 0x6C),
                new Instruction(0x64, 0x00)
            };

            Assert.Equal(expected, _assembler.Assemble("db 1,4,9,16,25,36,\"Hello World\""));
            Assert.Equal(expected, _assembler.Assemble("db 1,4,9,16,25,36,'Hello World'"));
        }

        [Theory]
        [InlineData(":")] // label delimiter
        [InlineData(";")] // comment delimiter
        [InlineData(",")] // operand delimiter
        public void ShouldAssembleDataByteInstructionWithDelimiterInsideQuotes(string delimiter)
        {
            _assembler.Assemble($"db \"{delimiter}\"");
            _assembler.Assemble($"db 1,\"{delimiter}\"");

            _assembler.Assemble($"db '{delimiter}'");
            _assembler.Assemble($"db 1,'{delimiter}'");
        }

        [Fact]
        public void ShouldAssembleHaltInstruction()
        {
            Assert.Equal(new[] { new Instruction(0xC0, 0x00) }, _assembler.Assemble("halt"));
        }

        [Fact]
        public void ShouldAssembleDirectStoreInstructions()
        {
            for (int number = 0; number <= byte.MaxValue; number++)
            {
                byte b = (byte)number;

                Assert.Equal(new[] { new Instruction(0x30, b) }, _assembler.Assemble($"store R0,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x31, b) }, _assembler.Assemble($"store R1,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x32, b) }, _assembler.Assemble($"store R2,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x33, b) }, _assembler.Assemble($"store R3,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x34, b) }, _assembler.Assemble($"store R4,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x35, b) }, _assembler.Assemble($"store R5,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x36, b) }, _assembler.Assemble($"store R6,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x37, b) }, _assembler.Assemble($"store R7,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x38, b) }, _assembler.Assemble($"store R8,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x39, b) }, _assembler.Assemble($"store R9,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x3A, b) }, _assembler.Assemble($"store RA,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x3B, b) }, _assembler.Assemble($"store RB,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x3C, b) }, _assembler.Assemble($"store RC,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x3D, b) }, _assembler.Assemble($"store RD,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x3E, b) }, _assembler.Assemble($"store RE,[{b}]"));
                Assert.Equal(new[] { new Instruction(0x3F, b) }, _assembler.Assemble($"store RF,[{b}]"));
            }
        }

        [Fact]
        public void ShouldAssembleIndirectStoreInstructions()
        {
            for (byte registerIndex = 0; registerIndex < 16; registerIndex++)
            {
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0x0, registerIndex).Combine()) }, _assembler.Assemble($"store R0,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0x1, registerIndex).Combine()) }, _assembler.Assemble($"store R1,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0x2, registerIndex).Combine()) }, _assembler.Assemble($"store R2,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0x3, registerIndex).Combine()) }, _assembler.Assemble($"store R3,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0x4, registerIndex).Combine()) }, _assembler.Assemble($"store R4,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0x5, registerIndex).Combine()) }, _assembler.Assemble($"store R5,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0x6, registerIndex).Combine()) }, _assembler.Assemble($"store R6,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0x7, registerIndex).Combine()) }, _assembler.Assemble($"store R7,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0x8, registerIndex).Combine()) }, _assembler.Assemble($"store R8,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0x9, registerIndex).Combine()) }, _assembler.Assemble($"store R9,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0xA, registerIndex).Combine()) }, _assembler.Assemble($"store RA,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0xB, registerIndex).Combine()) }, _assembler.Assemble($"store RB,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0xC, registerIndex).Combine()) }, _assembler.Assemble($"store RC,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0xD, registerIndex).Combine()) }, _assembler.Assemble($"store RD,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0xE, registerIndex).Combine()) }, _assembler.Assemble($"store RE,[R{registerIndex.ToHexString()}]"));
                Assert.Equal(new[] { new Instruction(0xE0, ((byte)0xF, registerIndex).Combine()) }, _assembler.Assemble($"store RF,[R{registerIndex.ToHexString()}]"));
            }
        }

        [Fact]
        public void ShouldAssembleMoveInstructions()
        {
            for (byte registerIndex = 0; registerIndex < 16; registerIndex++)
            {
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0x0).Combine()) }, _assembler.Assemble($"move R0,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0x1).Combine()) }, _assembler.Assemble($"move R1,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0x2).Combine()) }, _assembler.Assemble($"move R2,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0x3).Combine()) }, _assembler.Assemble($"move R3,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0x4).Combine()) }, _assembler.Assemble($"move R4,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0x5).Combine()) }, _assembler.Assemble($"move R5,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0x6).Combine()) }, _assembler.Assemble($"move R6,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0x7).Combine()) }, _assembler.Assemble($"move R7,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0x8).Combine()) }, _assembler.Assemble($"move R8,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0x9).Combine()) }, _assembler.Assemble($"move R9,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0xA).Combine()) }, _assembler.Assemble($"move RA,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0xB).Combine()) }, _assembler.Assemble($"move RB,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0xC).Combine()) }, _assembler.Assemble($"move RC,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0xD).Combine()) }, _assembler.Assemble($"move RD,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0xE).Combine()) }, _assembler.Assemble($"move RE,R{registerIndex.ToHexString()}"));
                Assert.Equal(new[] { new Instruction(0x40, (registerIndex, (byte)0xF).Combine()) }, _assembler.Assemble($"move RF,R{registerIndex.ToHexString()}"));
            }
        }

        [Fact]
        public void ShouldAssembleHelloWorldSampleProgram()
        {
            Assert.Equal(SamplePrograms.HelloWorldInstructions, _assembler.Assemble(SamplePrograms.HelloWorldCode));
        }

        [Fact]
        public void ShouldAssembleOutputTestSampleProgram()
        {
            Assert.Equal(SamplePrograms.OutputTestInstructions, _assembler.Assemble(SamplePrograms.OutputTestCode));
        }

        [Fact]
        public void ShouldAssembleTemplateSampleProgram()
        {
            Assert.Equal(SamplePrograms.TemplateInstructions, _assembler.Assemble(SamplePrograms.TemplateCode));
        }

        [Fact]
        public void ShouldAssembleProgramWithLabelUsageBeforeDeclaration()
        {
            Assert.Equal(new[] { new Instruction(0x21, 0x02), new Instruction(0x0A, 0x00) }, _assembler.Assemble("load    R1,Text\r\nText:    db      10"));
        }

        [Fact]
        public void ShouldAssembleDecimalLiterals()
        {
            Assert.Equal(new[] { new Instruction(0x01, 0x00) }, _assembler.Assemble("db 1d"));
            Assert.Equal(new[] { new Instruction(0x64, 0x00) }, _assembler.Assemble("db 100d"));
            Assert.Equal(new[] { new Instruction(0xFF, 0x00) }, _assembler.Assemble("db 255d"));
        }

        [Fact]
        public void ShouldAssembleNegativeDecimalLiterals()
        {
            Assert.Equal(new[] { new Instruction(0x80, 0x00) }, _assembler.Assemble("db -128"));
            Assert.Equal(new[] { new Instruction(0xAF, 0x00) }, _assembler.Assemble("db -81d"));
        }

        [Fact]
        public void ShouldAssembleBinaryLiterals()
        {
            Assert.Equal(new[] { new Instruction(0x01, 0x00) }, _assembler.Assemble("db 00000001b"));
            Assert.Equal(new[] { new Instruction(0x64, 0x00) }, _assembler.Assemble("db 01100100b"));
            Assert.Equal(new[] { new Instruction(0xFF, 0x00) }, _assembler.Assemble("db 11111111b"));
        }

        [Fact]
        public void ShouldAssembleHexLiterals()
        {
            Assert.Equal(new[] { new Instruction(0x01, 0x00) }, _assembler.Assemble("db 0x01"));
            Assert.Equal(new[] { new Instruction(0x64, 0x00) }, _assembler.Assemble("db 0x64"));
            Assert.Equal(new[] { new Instruction(0xFF, 0x00) }, _assembler.Assemble("db 0xFF"));

            Assert.Equal(new[] { new Instruction(0x01, 0x00) }, _assembler.Assemble("db $01"));
            Assert.Equal(new[] { new Instruction(0x64, 0x00) }, _assembler.Assemble("db $64"));
            Assert.Equal(new[] { new Instruction(0xFF, 0x00) }, _assembler.Assemble("db $FF"));

            Assert.Equal(new[] { new Instruction(0x01, 0x00) }, _assembler.Assemble("db 01h"));
            Assert.Equal(new[] { new Instruction(0x64, 0x00) }, _assembler.Assemble("db 64h"));
            Assert.Equal(new[] { new Instruction(0xFF, 0x00) }, _assembler.Assemble("db 0FFh"));
        }

        [Fact]
        public void ShouldIgnoreComments()
        {
            Assert.Equal(new[] { new Instruction(0x23, 0xAA) }, _assembler.Assemble("load R3, 0xAA; This is an immediate load instruction."));

            Assert.Equal(new Instruction[] { }, _assembler.Assemble(";load R3, 0xAA; This is a commented immediate load instruction."));

            Assert.Equal(new Instruction[] { }, _assembler.Assemble(";This is a comment with no instructions preceding it."));

            Assert.Equal(new Instruction[] { }, _assembler.Assemble(";This;comment;has;multiple;semicolons."));

            Assert.Equal(new Instruction[] { }, _assembler.Assemble(";"));
        }

        [Fact]
        public void ShouldAssembleOriginInstruction_WithOneOriginOnly()
        {
            Assert.Equal(new[]
            {
                new Instruction(0x00, 0x00),
                new Instruction(0x00, 0x00),
                new Instruction(0x00, 0x00),
                new Instruction(0x20, 0x02)
            },
            _assembler.Assemble(@"org 6
                                  load R0,2"));
        }

        [Fact]
        public void ShouldAssembleOriginInstruction_WithLowerOriginFollowingHigherOrigin()
        {
            Assert.Equal(new[]
            {
                new Instruction(0x00, 0x00),
                new Instruction(0x25, 0x01),
                new Instruction(0x00, 0x00),
                new Instruction(0x00, 0x00),
                new Instruction(0x00, 0x00),
                new Instruction(0x20, 0x02)
            },
            _assembler.Assemble(@"org 10
                                  load R0,2
                                  org 2
                                  load R5,1"));
        }

        [Fact]
        public void ShouldAssembleOriginInstruction_WithOriginPreceedingLabel()
        {
            Assert.Equal(new[]
            {
                new Instruction(0xB0, 0x0A),
                new Instruction(0x00, 0x00),
                new Instruction(0x00, 0x00),
                new Instruction(0x00, 0x00),
                new Instruction(0x00, 0x00),
            },
            _assembler.Assemble(@"jmp MyLabel
                                      org 10
                                      MyLabel:"));
        }
    }
}
