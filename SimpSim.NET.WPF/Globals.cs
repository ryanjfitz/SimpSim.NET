﻿namespace SimpSim.NET.WPF
{
    public static class Globals
    {
        public static readonly Memory Memory = new Memory();

        public static readonly Registers Registers = new Registers();

        public static readonly Machine Machine = new Machine(Memory, Registers);

        public static readonly Assembler Assembler = new Assembler();
    }
}
