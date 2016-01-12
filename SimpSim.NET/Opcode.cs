namespace SimpSim.NET
{
    internal enum Opcode : byte
    {
        ImmediateLoad = 0x2,
        DirectLoad = 0x1,
        DirectStore = 0x3,
        IndirectLoad = 0xD,
        IndirectStore = 0xE,
        Move = 0x4,
        IntegerAdd = 0x5,
        FloatingPointAdd = 0x6,
        Or = 0x7,
        And = 0x8,
        Xor = 0x9,
        Ror = 0xA,
        JumpEqual = 0xB,
        JumpLessEqual = 0xF,
        Halt = 0xC
    }
}