using System;
using System.Linq;

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
            string comment = "";

            string[] split = line.Split(new[] { CommentDelimiter }, 2);

            if (split.Length == 2)
            {
                line = split[0];
                comment = split[1].Trim();
            }

            return comment;
        }

        private static string GetLabel(ref string line)
        {
            string label = "";

            int delimiterIndex = line.IndexOf(LabelDelimiter);

            if (delimiterIndex > -1)
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

            if (Char.IsNumber(input[0]))
                return false;

            return true;
        }

        private static string GetMnemonic(string line)
        {
            string[] split = line.Trim().Split();

            string mnemonic = split[0];

            return mnemonic;
        }

        private static string[] GetOperands(string line)
        {
            string[] split = line.Trim().Split(null, 2);

            string[] operands;

            if (split.Length == 2)
                operands = split[1].Split(',').Select(o => o.Trim()).ToArray();
            else
                operands = new string[] { };

            return operands;
        }

        public override string ToString()
        {
            return $"{Mnemonic} {String.Join(",", Operands)}";
        }
    }
}