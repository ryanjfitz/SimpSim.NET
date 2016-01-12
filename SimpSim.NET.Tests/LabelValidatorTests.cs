using System.Linq;
using NUnit.Framework;

namespace SimpSim.NET.Tests
{
    [TestFixture]
    public class LabelValidatorTests
    {

        private LabelValidator _labelValidator;

        [SetUp]
        public void SetUp()
        {
            _labelValidator = new LabelValidator();
        }

        [Test]
        public void ShouldValidateValidLabels()
        {
            Assert.IsTrue(_labelValidator.Validate("Label:"));

            Assert.IsTrue(_labelValidator.Validate("NextChar2:"));

            Assert.IsTrue(_labelValidator.Validate("~MyLabel~:"));
        }

        [Test]
        public void ShouldNotValidateLabelsThatContainInvalidCharacters()
        {
            var allCharacters = Enumerable.Range(0, 256).Select(i => (char)i);

            var capitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            var numericCharacters = "0123456789";
            var specialCharacters = "#_~";

            var invalidCharacters = allCharacters.Except(capitalLetters).Except(lowercaseLetters).Except(numericCharacters).Except(specialCharacters);

            foreach (char c in invalidCharacters)
                Assert.IsFalse(_labelValidator.Validate(c + ":"));
        }

        [Test]
        public void ShouldNotValidateLabelsThatStartWithNumber()
        {
            Assert.IsFalse(_labelValidator.Validate("1Label:"));

            Assert.IsFalse(_labelValidator.Validate("2NextChar2"));

            Assert.IsFalse(_labelValidator.Validate("31~MyLabel~"));
        }

        [Test]
        public void ShouldValidateIfNoLabel()
        {
            Assert.IsTrue(_labelValidator.Validate(""));
        }

        [Test]
        public void ShouldNotValidateLabelThatDoesNotEndWithColon()
        {
            Assert.IsFalse(_labelValidator.Validate("MyLabel"));
        }

        [Test]
        public void ShouldNotValidateIfColonOnly()
        {
            Assert.IsFalse(_labelValidator.Validate(":"));
        }

    }

    public class LabelValidator
    {
        public bool Validate(string line)
        {
            if (line.Length == 0)
                return true;

            if (line == ":")
                return false;

            if (!line.EndsWith(":"))
                return false;

            foreach (char c in line.Substring(0, line.Length - 1))
                if (!"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789#_~".Contains(c))
                    return false;

            if (char.IsNumber(line[0]))
                return false;

            return true;
        }
    }
}
