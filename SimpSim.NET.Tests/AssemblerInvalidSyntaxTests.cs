using NUnit.Framework;

namespace SimpSim.NET.Tests
{
    public class AssemblerInvalidSyntaxTests
    {
        private Assembler _assembler;

        [SetUp]
        public void SetUp()
        {
            _assembler = new Assembler();
        }

        [Test]
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

        [Test]
        public void ShouldNotAssembleHexLiteralAssemblySyntaxThatStartsWithLetter()
        {
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("db FFh"));
        }

        [Test]
        public void ShouldNotAssembleUnrecognizedMnemonic()
        {
            Assert.Throws<UnrecognizedMnemonicException>(() => _assembler.Assemble("abc R0,0"));
        }

        [Test]
        public void ShouldNotAssembleDataByteInstructionWithRegisterOrNoOperands()
        {
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("db R0"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("db R1,R2"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("db"));
        }

        [Test]
        public void ShouldNotAssembleOriginInstructionWithoutSingleNumberOperand()
        {
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("org R0"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("org 'Hello World!'"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("org 10,12"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("org"));
        }

        [Test]
        public void ShouldNotAssembleJumpInstructionWithoutSingleAddress()
        {
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("jmp R0"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("jmp 10,12"));
            Assert.Throws<AssemblyException>(() => _assembler.Assemble("jmp"));
        }

        [Test]
        public void ShouldNotAssembleJumpEqualInstructionThatDoesNotCompareWithR0()
        {
            for (byte leftRegisterIndex = 0; leftRegisterIndex < 16; leftRegisterIndex++)
                for (byte rightRegisterIndex = 1; rightRegisterIndex < 16; rightRegisterIndex++)
                    for (int number = 0; number <= byte.MaxValue; number++)
                        Assert.Throws<AssemblyException>(() => _assembler.Assemble($"jmpEQ R{leftRegisterIndex.ToHexString()}=R{rightRegisterIndex.ToHexString()},{number}"));
        }
    }
}
