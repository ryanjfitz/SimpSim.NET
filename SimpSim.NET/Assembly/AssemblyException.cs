using System;

namespace SimpSim.NET;

public class AssemblyException : Exception
{
    public override string Message { get; }
    public int LineNumber { get; }

    public AssemblyException(string message, int lineNumber) : base(message)
    {
        Message = message;
        LineNumber = lineNumber;
    }
}

public class LabelAssemblyException : AssemblyException
{
    public LabelAssemblyException(string label, int lineNumber) : base("Invalid or undefined label: " + label, lineNumber)
    {
    }
}

public class UnrecognizedMnemonicException : AssemblyException
{
    public UnrecognizedMnemonicException(int lineNumber) : base("Unrecognized mnemonic.", lineNumber)
    {
    }
}