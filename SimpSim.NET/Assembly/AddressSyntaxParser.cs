namespace SimpSim.NET
{
    internal class AddressSyntaxParser
    {
        private readonly SymbolTable _symbolTable;

        public AddressSyntaxParser(SymbolTable symbolTable)
        {
            _symbolTable = symbolTable;
        }

        public bool TryParse(string input, out AddressSyntax addressSyntax, BracketExpectation bracketExpectation = BracketExpectation.NotPresent)
        {
            bool isSuccess;

            bool isAddress = IsAddress(input.Trim('[', ']'), out byte address, out string undefinedLabel);

            bool isSurroundedByBrackets = input.StartsWith("[") && input.EndsWith("]");

            if (bracketExpectation == BracketExpectation.Present)
                isSuccess = isAddress && isSurroundedByBrackets;
            else
                isSuccess = isAddress && !isSurroundedByBrackets;

            if (isSuccess)
                addressSyntax = new AddressSyntaxImpl(address, undefinedLabel);
            else
                addressSyntax = null;

            return isSuccess;
        }

        private bool IsAddress(string input, out byte address, out string undefinedLabel)
        {
            undefinedLabel = null;

            if (NumberSyntax.TryParse(input, out address))
                return true;

            if (IsRegister(input))
                return false;

            if (_symbolTable.ContainsLabel(input))
                address = _symbolTable[input];
            else
                undefinedLabel = input;

            return true;
        }

        private bool IsRegister(string input)
        {
            return RegisterSyntax.TryParse(input, out _);
        }

        private class AddressSyntaxImpl : AddressSyntax
        {
            public byte Value { get; }

            public string UndefinedLabel { get; }

            public bool ContainsUndefinedLabel => !string.IsNullOrWhiteSpace(UndefinedLabel);

            public AddressSyntaxImpl(byte value, string undefinedLabel)
            {
                Value = value;
                UndefinedLabel = undefinedLabel;
            }

            public override string ToString()
            {
                return ContainsUndefinedLabel ? UndefinedLabel : Value.ToString();
            }
        }
    }
}