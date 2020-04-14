using System.Text.RegularExpressions;

namespace SimpSim.NET
{
    internal class RegisterSyntax
    {
        private string Value { get; }

        private RegisterSyntax(string value)
        {
            Value = value;
        }

        public byte Index => Value[1].ToString().ToByteFromHex();

        public static bool TryParse(string input, out RegisterSyntax registerSyntax, BracketExpectation bracketExpectation = BracketExpectation.NotPresent)
        {
            bool isSuccess;

            bool isRegister = Regex.IsMatch(input, @"^\[?R[0-9A-F]\]?$");

            bool isSurroundedByBrackets = input.StartsWith("[") && input.EndsWith("]");

            if (bracketExpectation == BracketExpectation.Present)
                isSuccess = isRegister && isSurroundedByBrackets;
            else
                isSuccess = isRegister && !isSurroundedByBrackets;

            if (isSuccess)
                registerSyntax = new RegisterSyntax(input.Trim('[', ']'));
            else
                registerSyntax = null;

            return isSuccess;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}