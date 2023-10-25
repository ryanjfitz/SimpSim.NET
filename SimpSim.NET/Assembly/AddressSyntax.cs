namespace SimpSim.NET.Assembly;

internal class AddressSyntax
{
    public byte Value { get; }

    public string UndefinedLabel { get; }

    public bool ContainsUndefinedLabel => !string.IsNullOrWhiteSpace(UndefinedLabel);

    private AddressSyntax(byte value, string undefinedLabel)
    {
        Value = value;
        UndefinedLabel = undefinedLabel;
    }

    public static bool TryParse(string input, SymbolTable symbolTable, out AddressSyntax addressSyntax, BracketExpectation bracketExpectation = BracketExpectation.NotPresent)
    {
        bool isSuccess;

        bool isAddress = IsAddress(input.Trim('[', ']'), symbolTable, out byte address, out string undefinedLabel);

        bool isSurroundedByBrackets = input.StartsWith("[") && input.EndsWith("]");

        if (bracketExpectation == BracketExpectation.Present)
            isSuccess = isAddress && isSurroundedByBrackets;
        else
            isSuccess = isAddress && !isSurroundedByBrackets;

        if (isSuccess)
            addressSyntax = new AddressSyntax(address, undefinedLabel);
        else
            addressSyntax = null;

        return isSuccess;
    }

    private static bool IsAddress(string input, SymbolTable symbolTable, out byte address, out string undefinedLabel)
    {
        undefinedLabel = null;

        if (NumberSyntax.TryParse(input, out address))
            return true;

        if (IsRegister(input))
            return false;

        if (symbolTable.ContainsLabel(input))
            address = symbolTable[input];
        else
            undefinedLabel = input;

        return true;
    }

    private static bool IsRegister(string input)
    {
        return RegisterSyntax.TryParse(input, out _);
    }

    public override string ToString()
    {
        return ContainsUndefinedLabel ? UndefinedLabel : Value.ToString();
    }
}