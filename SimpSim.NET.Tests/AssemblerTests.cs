using NUnit.Framework;

namespace SimpSim.NET.Tests
{
    [TestFixture]
    public class AssemblerTests
    {
        private Assembler _assembler;

        [SetUp]
        public void SetUp()
        {
            _assembler = new Assembler();
        }

        [Test]
        public void ShouldAssembleImmediateLoadInstructions()
        {
            for (int number = 0; number <= byte.MaxValue; number++)
            {
                byte b = (byte)number;

                CollectionAssert.AreEqual(new[] { new Instruction(0x20, b) }, _assembler.Assemble($"load R0,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x21, b) }, _assembler.Assemble($"load R1,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x22, b) }, _assembler.Assemble($"load R2,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x23, b) }, _assembler.Assemble($"load R3,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x24, b) }, _assembler.Assemble($"load R4,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x25, b) }, _assembler.Assemble($"load R5,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x26, b) }, _assembler.Assemble($"load R6,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x27, b) }, _assembler.Assemble($"load R7,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x28, b) }, _assembler.Assemble($"load R8,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x29, b) }, _assembler.Assemble($"load R9,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x2A, b) }, _assembler.Assemble($"load RA,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x2B, b) }, _assembler.Assemble($"load RB,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x2C, b) }, _assembler.Assemble($"load RC,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x2D, b) }, _assembler.Assemble($"load RD,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x2E, b) }, _assembler.Assemble($"load RE,{b}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x2F, b) }, _assembler.Assemble($"load RF,{b}"));
            }
        }

        [Test]
        public void ShouldAssembleDirectLoadInstructions()
        {
            for (int number = 0; number <= byte.MaxValue; number++)
            {
                byte b = (byte)number;

                CollectionAssert.AreEqual(new[] { new Instruction(0x10, b) }, _assembler.Assemble($"load R0,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x11, b) }, _assembler.Assemble($"load R1,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x12, b) }, _assembler.Assemble($"load R2,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x13, b) }, _assembler.Assemble($"load R3,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x14, b) }, _assembler.Assemble($"load R4,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x15, b) }, _assembler.Assemble($"load R5,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x16, b) }, _assembler.Assemble($"load R6,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x17, b) }, _assembler.Assemble($"load R7,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x18, b) }, _assembler.Assemble($"load R8,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x19, b) }, _assembler.Assemble($"load R9,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x1A, b) }, _assembler.Assemble($"load RA,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x1B, b) }, _assembler.Assemble($"load RB,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x1C, b) }, _assembler.Assemble($"load RC,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x1D, b) }, _assembler.Assemble($"load RD,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x1E, b) }, _assembler.Assemble($"load RE,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x1F, b) }, _assembler.Assemble($"load RF,[{b}]"));
            }
        }

        [Test]
        public void ShouldAssembleIndirectLoadInstructions()
        {
            for (byte registerIndex = 0; registerIndex < 16; registerIndex++)
            {
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0x0, registerIndex)) }, _assembler.Assemble($"load R0,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0x1, registerIndex)) }, _assembler.Assemble($"load R1,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0x2, registerIndex)) }, _assembler.Assemble($"load R2,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0x3, registerIndex)) }, _assembler.Assemble($"load R3,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0x4, registerIndex)) }, _assembler.Assemble($"load R4,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0x5, registerIndex)) }, _assembler.Assemble($"load R5,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0x6, registerIndex)) }, _assembler.Assemble($"load R6,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0x7, registerIndex)) }, _assembler.Assemble($"load R7,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0x8, registerIndex)) }, _assembler.Assemble($"load R8,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0x9, registerIndex)) }, _assembler.Assemble($"load R9,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0xA, registerIndex)) }, _assembler.Assemble($"load RA,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0xB, registerIndex)) }, _assembler.Assemble($"load RB,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0xC, registerIndex)) }, _assembler.Assemble($"load RC,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0xD, registerIndex)) }, _assembler.Assemble($"load RD,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0xE, registerIndex)) }, _assembler.Assemble($"load RE,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xD0, ByteUtilities.GetByteFromNibbles(0xF, registerIndex)) }, _assembler.Assemble($"load RF,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
            }
        }

        [Test]
        public void ShouldAssembleAddIntegerInstructions()
        {
            for (byte i = 0; i < 16; i++)
                for (byte j = 0; j < 16; j++)
                    for (byte k = 0; k < 16; k++)
                    {
                        Instruction[] expected = { new Instruction(ByteUtilities.GetByteFromNibbles(0x5, i), ByteUtilities.GetByteFromNibbles(j, k)) };
                        Instruction[] actual = _assembler.Assemble($"addi R{ByteUtilities.ConvertByteToHexString(i)},R{ByteUtilities.ConvertByteToHexString(j)},R{ByteUtilities.ConvertByteToHexString(k)}");

                        CollectionAssert.AreEqual(expected, actual);
                    }
        }

        [Test]
        public void ShouldAssembleAddFloatInstructions()
        {
            for (byte i = 0; i < 16; i++)
                for (byte j = 0; j < 16; j++)
                    for (byte k = 0; k < 16; k++)
                    {
                        Instruction[] expected = { new Instruction(ByteUtilities.GetByteFromNibbles(0x6, i), ByteUtilities.GetByteFromNibbles(j, k)) };
                        Instruction[] actual = _assembler.Assemble($"addf R{ByteUtilities.ConvertByteToHexString(i)},R{ByteUtilities.ConvertByteToHexString(j)},R{ByteUtilities.ConvertByteToHexString(k)}");

                        CollectionAssert.AreEqual(expected, actual);
                    }
        }

        [Test]
        public void ShouldAssembleBitwiseAndInstructions()
        {
            for (byte i = 0; i < 16; i++)
                for (byte j = 0; j < 16; j++)
                    for (byte k = 0; k < 16; k++)
                    {
                        Instruction[] expected = { new Instruction(ByteUtilities.GetByteFromNibbles(0x8, i), ByteUtilities.GetByteFromNibbles(j, k)) };
                        Instruction[] actual = _assembler.Assemble($"and R{ByteUtilities.ConvertByteToHexString(i)},R{ByteUtilities.ConvertByteToHexString(j)},R{ByteUtilities.ConvertByteToHexString(k)}");

                        CollectionAssert.AreEqual(expected, actual);
                    }
        }

        [Test]
        public void ShouldAssembleBitwiseOrInstructions()
        {
            for (byte i = 0; i < 16; i++)
                for (byte j = 0; j < 16; j++)
                    for (byte k = 0; k < 16; k++)
                    {
                        Instruction[] expected = { new Instruction(ByteUtilities.GetByteFromNibbles(0x7, i), ByteUtilities.GetByteFromNibbles(j, k)) };
                        Instruction[] actual = _assembler.Assemble($"or R{ByteUtilities.ConvertByteToHexString(i)},R{ByteUtilities.ConvertByteToHexString(j)},R{ByteUtilities.ConvertByteToHexString(k)}");

                        CollectionAssert.AreEqual(expected, actual);
                    }
        }

        [Test]
        public void ShouldAssembleBitwiseExclusiveOrInstructions()
        {
            for (byte i = 0; i < 16; i++)
                for (byte j = 0; j < 16; j++)
                    for (byte k = 0; k < 16; k++)
                    {
                        Instruction[] expected = { new Instruction(ByteUtilities.GetByteFromNibbles(0x9, i), ByteUtilities.GetByteFromNibbles(j, k)) };
                        Instruction[] actual = _assembler.Assemble($"xor R{ByteUtilities.ConvertByteToHexString(i)},R{ByteUtilities.ConvertByteToHexString(j)},R{ByteUtilities.ConvertByteToHexString(k)}");

                        CollectionAssert.AreEqual(expected, actual);
                    }
        }

        [Test]
        public void ShouldAssembleRorInstructions()
        {
            for (byte number = 0; number < 16; number++)
            {
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0x0), number) }, _assembler.Assemble($"ror R0,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0x1), number) }, _assembler.Assemble($"ror R1,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0x2), number) }, _assembler.Assemble($"ror R2,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0x3), number) }, _assembler.Assemble($"ror R3,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0x4), number) }, _assembler.Assemble($"ror R4,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0x5), number) }, _assembler.Assemble($"ror R5,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0x6), number) }, _assembler.Assemble($"ror R6,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0x7), number) }, _assembler.Assemble($"ror R7,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0x8), number) }, _assembler.Assemble($"ror R8,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0x9), number) }, _assembler.Assemble($"ror R9,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0xA), number) }, _assembler.Assemble($"ror RA,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0xB), number) }, _assembler.Assemble($"ror RB,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0xC), number) }, _assembler.Assemble($"ror RC,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0xD), number) }, _assembler.Assemble($"ror RD,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0xE), number) }, _assembler.Assemble($"ror RE,{number}"));
                CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xA, 0xF), number) }, _assembler.Assemble($"ror RF,{number}"));
            }
        }

        [Test]
        public void ShouldNotAssembleRorInstructionsWithNumberGreaterThan15()
        {
            const byte number = 16;

            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror R0,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror R1,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror R2,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror R3,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror R4,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror R5,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror R6,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror R7,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror R8,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror R9,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror RA,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror RB,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror RC,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror RD,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror RE,{number}"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble($"ror RF,{number}"));
        }

        [Test]
        public void ShouldAssembleJumpEqualInstructions()
        {
            for (byte registerIndex = 0; registerIndex < 16; registerIndex++)
                for (int number = 0; number <= byte.MaxValue; number++)
                    CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xB, registerIndex), (byte)number) }, _assembler.Assemble($"jmpEQ R{ByteUtilities.ConvertByteToHexString(registerIndex)}=R0,{number}"));
        }

        [Test]
        public void ShouldAssembleJumpLessEqualInstructions()
        {
            for (byte registerIndex = 0; registerIndex < 16; registerIndex++)
                for (int number = 0; number <= byte.MaxValue; number++)
                    CollectionAssert.AreEqual(new[] { new Instruction(ByteUtilities.GetByteFromNibbles(0xF, registerIndex), (byte)number) }, _assembler.Assemble($"jmpLE R{ByteUtilities.ConvertByteToHexString(registerIndex)}<=R0,{number}"));
        }

        [Test]
        public void ShouldAssembleJumpInstructions()
        {
            for (int number = 0; number <= byte.MaxValue; number++)
                CollectionAssert.AreEqual(new[] { new Instruction(0xB0, (byte)number) }, _assembler.Assemble($"jmp {number}"));
        }

        [Test]
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

            CollectionAssert.AreEqual(expected, _assembler.Assemble("db 1,4,9,16,25,36,\"Hello World\""));
            CollectionAssert.AreEqual(expected, _assembler.Assemble("db 1,4,9,16,25,36,'Hello World'"));
        }

        [Test]
        public void ShouldAssembleHaltInstruction()
        {
            CollectionAssert.AreEqual(new[] { new Instruction(0xC0, 0x00) }, _assembler.Assemble("halt"));
        }

        [Test]
        public void ShouldAssembleDirectStoreInstructions()
        {
            for (int number = 0; number <= byte.MaxValue; number++)
            {
                byte b = (byte)number;

                CollectionAssert.AreEqual(new[] { new Instruction(0x30, b) }, _assembler.Assemble($"store R0,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x31, b) }, _assembler.Assemble($"store R1,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x32, b) }, _assembler.Assemble($"store R2,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x33, b) }, _assembler.Assemble($"store R3,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x34, b) }, _assembler.Assemble($"store R4,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x35, b) }, _assembler.Assemble($"store R5,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x36, b) }, _assembler.Assemble($"store R6,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x37, b) }, _assembler.Assemble($"store R7,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x38, b) }, _assembler.Assemble($"store R8,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x39, b) }, _assembler.Assemble($"store R9,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x3A, b) }, _assembler.Assemble($"store RA,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x3B, b) }, _assembler.Assemble($"store RB,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x3C, b) }, _assembler.Assemble($"store RC,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x3D, b) }, _assembler.Assemble($"store RD,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x3E, b) }, _assembler.Assemble($"store RE,[{b}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x3F, b) }, _assembler.Assemble($"store RF,[{b}]"));
            }
        }

        [Test]
        public void ShouldAssembleIndirectStoreInstructions()
        {
            for (byte registerIndex = 0; registerIndex < 16; registerIndex++)
            {
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0x0, registerIndex)) }, _assembler.Assemble($"store R0,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0x1, registerIndex)) }, _assembler.Assemble($"store R1,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0x2, registerIndex)) }, _assembler.Assemble($"store R2,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0x3, registerIndex)) }, _assembler.Assemble($"store R3,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0x4, registerIndex)) }, _assembler.Assemble($"store R4,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0x5, registerIndex)) }, _assembler.Assemble($"store R5,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0x6, registerIndex)) }, _assembler.Assemble($"store R6,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0x7, registerIndex)) }, _assembler.Assemble($"store R7,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0x8, registerIndex)) }, _assembler.Assemble($"store R8,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0x9, registerIndex)) }, _assembler.Assemble($"store R9,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0xA, registerIndex)) }, _assembler.Assemble($"store RA,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0xB, registerIndex)) }, _assembler.Assemble($"store RB,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0xC, registerIndex)) }, _assembler.Assemble($"store RC,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0xD, registerIndex)) }, _assembler.Assemble($"store RD,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0xE, registerIndex)) }, _assembler.Assemble($"store RE,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
                CollectionAssert.AreEqual(new[] { new Instruction(0xE0, ByteUtilities.GetByteFromNibbles(0xF, registerIndex)) }, _assembler.Assemble($"store RF,[R{ByteUtilities.ConvertByteToHexString(registerIndex)}]"));
            }
        }

        [Test]
        public void ShouldAssembleMoveInstructions()
        {
            for (byte registerIndex = 0; registerIndex < 16; registerIndex++)
            {
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0x0, registerIndex)) }, _assembler.Assemble($"move R0,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0x1, registerIndex)) }, _assembler.Assemble($"move R1,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0x2, registerIndex)) }, _assembler.Assemble($"move R2,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0x3, registerIndex)) }, _assembler.Assemble($"move R3,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0x4, registerIndex)) }, _assembler.Assemble($"move R4,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0x5, registerIndex)) }, _assembler.Assemble($"move R5,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0x6, registerIndex)) }, _assembler.Assemble($"move R6,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0x7, registerIndex)) }, _assembler.Assemble($"move R7,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0x8, registerIndex)) }, _assembler.Assemble($"move R8,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0x9, registerIndex)) }, _assembler.Assemble($"move R9,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0xA, registerIndex)) }, _assembler.Assemble($"move RA,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0xB, registerIndex)) }, _assembler.Assemble($"move RB,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0xC, registerIndex)) }, _assembler.Assemble($"move RC,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0xD, registerIndex)) }, _assembler.Assemble($"move RD,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0xE, registerIndex)) }, _assembler.Assemble($"move RE,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
                CollectionAssert.AreEqual(new[] { new Instruction(0x40, ByteUtilities.GetByteFromNibbles(0xF, registerIndex)) }, _assembler.Assemble($"move RF,R{ByteUtilities.ConvertByteToHexString(registerIndex)}"));
            }
        }

        [Test]
        public void ShouldAssembleHelloWorldSampleProgram()
        {
            CollectionAssert.AreEqual(SamplePrograms.HelloWorldInstructions, _assembler.Assemble(SamplePrograms.HelloWorldCode));
        }

        [Test]
        public void ShouldAssembleOutputTestSampleProgram()
        {
            CollectionAssert.AreEqual(SamplePrograms.OutputTestInstructions, _assembler.Assemble(SamplePrograms.OutputTestCode));
        }

        [Test]
        public void ShouldAssembleTemplateSampleProgram()
        {
            CollectionAssert.AreEqual(SamplePrograms.TemplateInstructions, _assembler.Assemble(SamplePrograms.TemplateCode));
        }

        [Test]
        public void ShouldAssembleProgramWithLabelUsageBeforeDeclaration()
        {
            CollectionAssert.AreEqual(new[] { new Instruction(0x21, 0x02), new Instruction(0x0A, 0x00) }, _assembler.Assemble("load    R1,Text\r\nText:    db      10"));
        }

        [Test]
        public void ShouldAssembleDecimalLiterals()
        {
            CollectionAssert.AreEqual(new[] { new Instruction(0x01, 0x00) }, _assembler.Assemble("db 1d"));
            CollectionAssert.AreEqual(new[] { new Instruction(0x64, 0x00) }, _assembler.Assemble("db 100d"));
            CollectionAssert.AreEqual(new[] { new Instruction(0xFF, 0x00) }, _assembler.Assemble("db 255d"));
        }

        [Test]
        public void ShouldAssembleNegativeDecimalLiterals()
        {
            CollectionAssert.AreEqual(new[] { new Instruction(0x80, 0x00) }, _assembler.Assemble("db -128"));
            CollectionAssert.AreEqual(new[] { new Instruction(0xAF, 0x00) }, _assembler.Assemble("db -81d"));
        }

        [Test]
        public void ShouldAssembleBinaryLiterals()
        {
            CollectionAssert.AreEqual(new[] { new Instruction(0x01, 0x00) }, _assembler.Assemble("db 00000001b"));
            CollectionAssert.AreEqual(new[] { new Instruction(0x64, 0x00) }, _assembler.Assemble("db 01100100b"));
            CollectionAssert.AreEqual(new[] { new Instruction(0xFF, 0x00) }, _assembler.Assemble("db 11111111b"));
        }

        [Test]
        public void ShouldAssembleHexLiterals()
        {
            CollectionAssert.AreEqual(new[] { new Instruction(0x01, 0x00) }, _assembler.Assemble("db 0x01"));
            CollectionAssert.AreEqual(new[] { new Instruction(0x64, 0x00) }, _assembler.Assemble("db 0x64"));
            CollectionAssert.AreEqual(new[] { new Instruction(0xFF, 0x00) }, _assembler.Assemble("db 0xFF"));

            CollectionAssert.AreEqual(new[] { new Instruction(0x01, 0x00) }, _assembler.Assemble("db $01"));
            CollectionAssert.AreEqual(new[] { new Instruction(0x64, 0x00) }, _assembler.Assemble("db $64"));
            CollectionAssert.AreEqual(new[] { new Instruction(0xFF, 0x00) }, _assembler.Assemble("db $FF"));

            CollectionAssert.AreEqual(new[] { new Instruction(0x01, 0x00) }, _assembler.Assemble("db 01h"));
            CollectionAssert.AreEqual(new[] { new Instruction(0x64, 0x00) }, _assembler.Assemble("db 64h"));
            CollectionAssert.AreEqual(new[] { new Instruction(0xFF, 0x00) }, _assembler.Assemble("db 0FFh"));
        }

        [Test]
        public void ShouldNotAssembleHexLiteralAssemblySyntaxThatStartsWithLetter()
        {
            CollectionAssert.AreEqual(new[] { new Instruction(0xCD, 0x00) }, _assembler.Assemble("db 0CDh"));
            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble("db CDh"));
        }

        [Test]
        public void ShouldIgnoreComments()
        {
            CollectionAssert.AreEqual(new[] { new Instruction(0x23, 0xAA) }, _assembler.Assemble("load R3, 0xAA; This is an immediate load instruction."));

            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble(";load R3, 0xAA; This is a commented immediate load instruction."));

            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble(";This is a comment with no instructions preceding it."));

            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble(";This;comment;has;multiple;semicolons."));

            CollectionAssert.AreEqual(new Instruction[] { }, _assembler.Assemble(";"));
        }

        [Test]
        public void ShouldAssembleOriginInstruction_WithOneOriginOnly()
        {
            CollectionAssert.AreEqual(new[]
            {
                new Instruction(0x00, 0x00),
                new Instruction(0x00, 0x00),
                new Instruction(0x00, 0x00),
                new Instruction(0x20, 0x02)
            },
            _assembler.Assemble(@"org 6
                                  load R0,2"));
        }

        [Test]
        public void ShouldAssembleOriginInstruction_WithLowerOriginFollowingHigherOrigin()
        {
            CollectionAssert.AreEqual(new[]
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

        [Test]
        public void ShouldAssembleOriginInstruction_WithOriginPreceedingLabel()
        {
            CollectionAssert.AreEqual(new[]
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
