namespace SimpSim.NET.Assembly;

internal static class StringLiteralSyntax
{
    public static bool TryParse(string input, out string stringLiteral)
    {
        if (input.Length > 1)
        {
            foreach (var (leftQuote, rightQuote) in GetQuotePairs())
            {
                if (input.First() == leftQuote && input.Last() == rightQuote)
                {
                    stringLiteral = input.TrimStart(leftQuote).TrimEnd(rightQuote);
                    return true;
                }
            }
        }

        stringLiteral = null;
        return false;
    }

    public static IEnumerable<(char, char)> GetQuotePairs()
    {
        return new[]
        {
            ('\"', '\"'),
            ('\'', '\''),
            ('“', '”'),
            ('‘', '’'),
        };
    }
}