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
    }
}
