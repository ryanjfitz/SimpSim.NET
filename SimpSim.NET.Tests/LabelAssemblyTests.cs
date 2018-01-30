using System.Linq;
using NUnit.Framework;

namespace SimpSim.NET.Tests
{
    [TestFixture]
    public class LabelAssemblyTests
    {
        private Assembler _assembler;

        [SetUp]
        public void SetUp()
        {
            _assembler = new Assembler();
        }

        [Test]
        public void ShouldValidateValidLabels()
        {
            Assert.DoesNotThrow(() => _assembler.Assemble("Label:"));

            Assert.DoesNotThrow(() => _assembler.Assemble("NextChar2:"));

            Assert.DoesNotThrow(() => _assembler.Assemble("~MyLabel~:"));
        }

        [Test]
        public void ShouldNotValidateLabelsThatContainInvalidCharacters()
        {
            var allCharacters = Enumerable.Range(0, 256).Select(i => (char)i);

            var capitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            var numericCharacters = "0123456789";
            var specialCharacters = "#_~";
            var commentCharacters = ";";

            var invalidCharacters =
                allCharacters
                .Except(capitalLetters)
                .Except(lowercaseLetters)
                .Except(numericCharacters)
                .Except(specialCharacters)
                .Except(commentCharacters);

            foreach (char c in invalidCharacters)
                Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble(c + ":"));
        }

        [Test]
        public void ShouldNotValidateLabelsThatStartWithNumber()
        {
            Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble("1Label:"));

            Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble("2NextChar2:"));

            Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble("31~MyLabel~:"));
        }

        [Test]
        public void ShouldValidateIfNoLabel()
        {
            Assert.DoesNotThrow(() => _assembler.Assemble(""));
        }

        [Test]
        public void ShouldNotValidateIfColonOnly()
        {
            Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble(":"));
            Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble("::"));
            Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble(":::"));
        }

        [Test]
        public void ShouldNotAssembleUndefinedLabel()
        {
            Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble("load R1,UndefinedLabel"));
        }
    }
}
