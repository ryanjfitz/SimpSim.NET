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
}