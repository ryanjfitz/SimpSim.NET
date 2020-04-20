using Xunit;

namespace SimpSim.NET.Tests
{
    public class AssemblerInvalidSyntaxTests
    {
        private readonly Assembler _assembler;

        public AssemblerInvalidSyntaxTests()
        {
            _assembler = new Assembler();
        }

        [Fact]
        public void ShouldNotAssembleRorInstructionsWithNumberGreaterThan15()
        {
            const byte number = 16;

            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror R0,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror R1,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror R2,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror R3,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror R4,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror R5,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror R6,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror R7,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror R8,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror R9,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror RA,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror RB,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror RC,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror RD,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror RE,{number}"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble($"ror RF,{number}"));
        }

        [Fact]
        public void ShouldNotAssembleHexLiteralAssemblySyntaxThatStartsWithLetter()
        {
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("db FFh"));
        }

        [Fact]
        public void ShouldNotAssembleUnrecognizedMnemonic()
        {
            Assert.Throws<UnrecognizedMnemonicException>(() => _assembler.Assemble("abc R0,0"));
        }

        [Fact]
        public void ShouldNotAssembleDataByteInstructionWithRegisterOrNoOperands()
        {
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("db R0"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("db R1,R2"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("db"));
        }

        [Fact]
        public void ShouldNotAssembleDataByteInstructionWithOneQuote()
        {
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("db '"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("db \""));
        }

        [Fact]
        public void ShouldNotAssembleOriginInstructionWithoutSingleNumberOperand()
        {
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("org R0"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("org 'Hello World!'"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("org 10,12"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("org"));
        }

        [Fact]
        public void ShouldNotAssembleJumpInstructionWithoutSingleAddress()
        {
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("jmp R0"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("jmp 10,12"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("jmp"));
        }

        [Fact]
        public void ShouldNotAssembleJumpEqualInstructionThatDoesNotCompareWithR0()
        {
            for (byte leftRegisterIndex = 0; leftRegisterIndex < 16; leftRegisterIndex++)
                for (byte rightRegisterIndex = 1; rightRegisterIndex < 16; rightRegisterIndex++)
                    for (int number = 0; number <= byte.MaxValue; number++)
                        Assert.Throws<AssemblyException>(() => _assembler.Assemble($"jmpEQ R{leftRegisterIndex.ToHexString()}=R{rightRegisterIndex.ToHexString()},{number}"));
        }
    }
}
