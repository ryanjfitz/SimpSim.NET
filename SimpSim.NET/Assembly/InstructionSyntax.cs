using System.Linq;
using System.Text.RegularExpressions;

namespace SimpSim.NET
{
    internal class InstructionSyntax
    {
        public string Comment { get; }
        public string Label { get; }
        public string Mnemonic { get; }
        public string[] Operands { get; }

        private const char CommentDelimiter = ';';
        private const char LabelDelimiter = ':';
        private const char OperandDelimiter = ',';

        private InstructionSyntax(string comment, string label, string mnemonic, string[] operands)
        {
            Comment = comment;
            Label = label;
            Mnemonic = mnemonic;
            Operands = operands;
        }

        public static InstructionSyntax Parse(string line)
        {
            string comment = GetComment(ref line);

            string label = GetLabel(ref line);

            string mnemonic = GetMnemonic(line);

            string[] operands = GetOperands(line);

            return new InstructionSyntax(comment, label, mnemonic, operands);
        }

        private static string GetComment(ref string line)
        {
            if (IsDelimiterBetweenQuotes(CommentDelimiter, line, out _))
                return "";

            string[] split = line.Split(new[] { CommentDelimiter }, 2);

            if (split.Length == 2)
            {
                line = split[0];
                return split[1].Trim();
            }

            return "";
        }

        private static string GetLabel(ref string line)
        {
            string label = "";

            if (!IsDelimiterBetweenQuotes(LabelDelimiter, line, out int delimiterIndex) && delimiterIndex > -1)
            {
                label = line.Substring(0, delimiterIndex + 1).Trim();
                line = line.Substring(delimiterIndex + 1);

                if (!IsValidLabel(label))
                    throw new LabelAssemblyException(label);

                label = label.TrimEnd(LabelDelimiter);
            }

            return label;
        }

        private static bool IsValidLabel(string input)
        {
            if (input.Length == 1 && input[0] == LabelDelimiter)
                return false;

            if (input.Last() != LabelDelimiter)
                return false;

            foreach (char c in input.TrimEnd(LabelDelimiter))
                if (!"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789#_~".Contains(c))
                    return false;

            if (char.IsNumber(input[0]))
                return false;

            return true;
        }

        private static string GetMnemonic(string line)
        {
            return line.Trim().Split()[0];
        }

        private static string[] GetOperands(string line)
        {
            string[] split = line.Trim().Split(null, 2);

            if (split.Length == 2)
            {
                string operands = split[1];

                string pattern = $"[^{OperandDelimiter}\\s\"']+|\"[^\"]*\"|'[^']*'";

                return Regex.Matches(operands, pattern).Select(match => match.Value.Trim()).ToArray();
            }

            return new string[] { };
        }

        private static bool IsDelimiterBetweenQuotes(char delimiter, string line, out int delimiterIndex)
        {
            delimiterIndex = line.IndexOf(delimiter);

            int firstSingleQuoteIndex = line.IndexOf("'");
            int lastSingleQuoteIndex = line.LastIndexOf("'");
            int firstDoubleQuoteIndex = line.IndexOf("\"");
            int lastDoubleQuoteIndex = line.LastIndexOf("\"");

            return (firstSingleQuoteIndex < delimiterIndex && delimiterIndex < lastSingleQuoteIndex) ||
                   (firstDoubleQuoteIndex < delimiterIndex && delimiterIndex < lastDoubleQuoteIndex);
        }

        public override string ToString()
        {
            return $"{Mnemonic} {string.Join(",", Operands)}";
        }
    }
}
