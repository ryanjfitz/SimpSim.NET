namespace SimpSim.NET
{
    public static class SamplePrograms
    {
        public const string HelloWorldCode
            = @"load    R1,Text
                load    R2,1
                load    R0,0
       NextChar:load    RF,[R1]
                addi    R1,R1,R2
                jmpEQ   RF=R0,Ready
                jmp     NextChar
       Ready:   halt

       Text:    db      10
                db      ""Hello world !!"",10
                db      ""    from the"",10
                db      ""  Simple Simulator"",10
                db      0";

        public static readonly Instruction[] HelloWorldInstructions =
        {
            new Instruction(0x21, 0x10),
            new Instruction(0x22, 0x01),
            new Instruction(0x20, 0x00),
            new Instruction(0xD0, 0xF1),
            new Instruction(0x51, 0x12),
            new Instruction(0xBF, 0x0E),
            new Instruction(0xB0, 0x06),
            new Instruction(0xC0, 0x00),
            new Instruction(0x0A, 0x48),
            new Instruction(0x65, 0x6C),
            new Instruction(0x6C, 0x6F),
            new Instruction(0x20, 0x77),
            new Instruction(0x6F, 0x72),
            new Instruction(0x6C, 0x64),
            new Instruction(0x20, 0x21),
            new Instruction(0x21, 0x0A),
            new Instruction(0x20, 0x20),
            new Instruction(0x20, 0x20),
            new Instruction(0x66, 0x72),
            new Instruction(0x6F, 0x6D),
            new Instruction(0x20, 0x74),
            new Instruction(0x68, 0x65),
            new Instruction(0x0A, 0x20),
            new Instruction(0x20, 0x53),
            new Instruction(0x69, 0x6D),
            new Instruction(0x70, 0x6C),
            new Instruction(0x65, 0x20),
            new Instruction(0x53, 0x69),
            new Instruction(0x6D, 0x75),
            new Instruction(0x6C, 0x61),
            new Instruction(0x74, 0x6F),
            new Instruction(0x72, 0x0A),
            new Instruction(0x00, 0x00)
        };

        public const string OutputTestCode
            = @"load    RF,0
                load    R7,1
       NextChar:addi    RF,RF,R7
                jmp     NextChar";

        public static readonly Instruction[] OutputTestInstructions =
        {
            new Instruction(0x2F, 0x00),
            new Instruction(0x27, 0x01),
            new Instruction(0x5F, 0xF7),
            new Instruction(0xB0, 0x04)
        };

        public static readonly Instruction[] OutputTestNonInfiniteInstructions =
        {
            new Instruction(0x2F, 0x00),
            new Instruction(0x27, 0x01),
            new Instruction(0x20, 0xFF),
            new Instruction(0x5F, 0xF7),
            new Instruction(0xBF, 0x0C),
            new Instruction(0xB0, 0x06),
            new Instruction(0x0C, 0x00)
        };

        public static readonly Instruction[] TemplateInstructions =
        {
            new Instruction(0x22,0x00),
            new Instruction(0xB0,0x60),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x20,0x39),
            new Instruction(0x2A,0x30),
            new Instruction(0x2B,0x07),
            new Instruction(0x2C,0xF0),
            new Instruction(0x8E,0x2C),
            new Instruction(0xAE,0x04),
            new Instruction(0x5E,0xEA),
            new Instruction(0xFE,0x72),
            new Instruction(0x5E,0xEB),
            new Instruction(0x3E,0x9B),
            new Instruction(0x2C,0x0F),
            new Instruction(0x8E,0x2C),
            new Instruction(0x5E,0xEA),
            new Instruction(0xFE,0x7E),
            new Instruction(0x5E,0xEB),
            new Instruction(0x3E,0x9C),
            new Instruction(0x20,0x00),
            new Instruction(0x2A,0x90),
            new Instruction(0x2B,0x01),
            new Instruction(0xD0,0xFA),
            new Instruction(0xBF,0x8E),
            new Instruction(0x5A,0xAB),
            new Instruction(0xB0,0x86),
            new Instruction(0xC0,0x00),
            new Instruction(0x0A,0x41),
            new Instruction(0x6E,0x73),
            new Instruction(0x77,0x65),
            new Instruction(0x72,0x20),
            new Instruction(0x3A,0x20),
            new Instruction(0x24,0x3F),
            new Instruction(0x3F,0x00),
            new Instruction(0x00,0x00),
            new Instruction(0x0E,0x10),
            new Instruction(0x1A,0x06),
            new Instruction(0xFB,0x24),
            new Instruction(0x06,0x00)
        };
    }
}
