namespace SimpSim.NET
{
    internal interface AddressSyntax
    {
        byte Value { get; }
        string UndefinedLabel { get; }
        bool ContainsUndefinedLabel { get; }
    }
}