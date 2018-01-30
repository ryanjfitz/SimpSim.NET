using System;

namespace SimpSim.NET
{
    public class AssemblyException : Exception
    {
        public override string Message { get; }

        public AssemblyException(string message) : base(message)
        {
            Message = message;
        }
    }

    public class LabelAssemblyException : AssemblyException
    {
        public LabelAssemblyException(string label) : base("Invalid or undefined label: " + label)
        {
        }
    }

    public class UnrecognizedMnemonicException : AssemblyException
    {
        public UnrecognizedMnemonicException() : base("Unrecognized mnemonic.")
        {
        }
    }
}