using System;
using System.Globalization;
using System.Linq;

namespace SimpSim.NET
{
    internal static class NumberSyntax
    {
        public static bool TryParse(string input, out byte number)
        {
            return TryParseDecimalLiteral(input, out number) || TryParseBinaryLiteral(input, out number) || TryParseHexLiteral(input, out number);
        }

        private static bool TryParseDecimalLiteral(string input, out byte number)
        {
            bool success = SByte.TryParse(input.TrimEnd('d'), out sbyte signedNumber);

            if (signedNumber < 0)
                number = (byte)signedNumber;
            else
                success = Byte.TryParse(input.TrimEnd('d'), out number);

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
            bool success = Byte.TryParse(input.TrimStart("0x".ToCharArray()), NumberStyles.HexNumber, null, out number)
                           || Byte.TryParse(input.TrimStart('$'), NumberStyles.HexNumber, null, out number)
                           || Byte.TryParse(input.TrimEnd('h'), NumberStyles.HexNumber, null, out number) && !Char.IsLetter(input.FirstOrDefault());

            if (!success)
                number = 0;

            return success;
        }
    }
}