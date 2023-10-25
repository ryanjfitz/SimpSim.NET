using System;
using System.Globalization;
using System.Linq;

namespace SimpSim.NET.Assembly;

internal static class NumberSyntax
{
    public static bool TryParse(string input, out byte number)
    {
        return TryParseDecimalLiteral(input, out number) || TryParseBinaryLiteral(input, out number) || TryParseHexLiteral(input, out number);
    }

    private static bool TryParseDecimalLiteral(string input, out byte number)
    {
        bool success = sbyte.TryParse(input.TrimEnd('d'), out sbyte signedNumber);

        if (signedNumber < 0)
            number = (byte)signedNumber;
        else
            success = byte.TryParse(input.TrimEnd('d'), out number);

        return success;
    }

    private static bool TryParseBinaryLiteral(string input, out byte number)
    {
        bool success;

        try
        {
            number = Convert.ToByte(input.TrimEnd('b'), 2);
            success = true;
        }
        catch
        {
            number = 0;
            success = false;
        }

        return success;
    }

    private static bool TryParseHexLiteral(string input, out byte number)
    {
        bool success = byte.TryParse(input.TrimStart("0x".ToCharArray()), NumberStyles.HexNumber, null, out number)
                       || byte.TryParse(input.TrimStart('$'), NumberStyles.HexNumber, null, out number)
                       || byte.TryParse(input.TrimEnd('h'), NumberStyles.HexNumber, null, out number) && !char.IsLetter(input.FirstOrDefault());

        if (!success)
            number = 0;

        return success;
    }
}