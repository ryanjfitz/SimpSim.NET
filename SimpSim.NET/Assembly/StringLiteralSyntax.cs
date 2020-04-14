using System.Linq;

namespace SimpSim.NET
{
    internal static class StringLiteralSyntax
    {
        public static bool TryParse(string input, out string stringLiteral)
        {
            const char doubleQuote = '"';
            const char singleQuote = '\'';

            if (input.First() == doubleQuote && input.Last() == doubleQuote)
            {
                stringLiteral = input.TrimStart(doubleQuote).TrimEnd(doubleQuote);
                return true;
            }

            if (input.First() == singleQuote && input.Last() == singleQuote)
            {
                stringLiteral = input.TrimStart(singleQuote).TrimEnd(singleQuote);
                return true;
            }

            stringLiteral = null;
            return false;
        }
    }
}