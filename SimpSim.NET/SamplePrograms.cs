using Ardalis.SmartEnum;

namespace SimpSim.NET;

public class SamplePrograms : SmartEnum<SamplePrograms>
{
    public static readonly SamplePrograms HelloWorld = new(
        "Hello World",
        1,
        """
                        load    R1,Text     ;the start of the string
                        load    R2,1        ;increase step
                        load    R0,0        ;string-terminator
               NextChar:load    RF,[R1]     ;get character and print it on screen
                        addi    R1,R1,R2    ;increase address
                        jmpEQ   RF=R0,Ready ;when string-terminator, then ready
                        jmp     NextChar    ;next character
               Ready:   halt
               
               Text:    db      10
                        db      "Hello world !!",10
                        db      "    from the",10
                        db      "  Simple Simulator",10
                        db      0           ;string-terminator
        """,
        new Instruction[]
        {
            new(0x21, 0x10),
            new(0x22, 0x01),
            new(0x20, 0x00),
            new(0xD0, 0xF1),
            new(0x51, 0x12),
            new(0xBF, 0x0E),
            new(0xB0, 0x06),
            new(0xC0, 0x00),
            new(0x0A, 0x48),
            new(0x65, 0x6C),
            new(0x6C, 0x6F),
            new(0x20, 0x77),
            new(0x6F, 0x72),
            new(0x6C, 0x64),
            new(0x20, 0x21),
            new(0x21, 0x0A),
            new(0x20, 0x20),
            new(0x20, 0x20),
            new(0x66, 0x72),
            new(0x6F, 0x6D),
            new(0x20, 0x74),
            new(0x68, 0x65),
            new(0x0A, 0x20),
            new(0x20, 0x53),
            new(0x69, 0x6D),
            new(0x70, 0x6C),
            new(0x65, 0x20),
            new(0x53, 0x69),
            new(0x6D, 0x75),
            new(0x6C, 0x61),
            new(0x74, 0x6F),
            new(0x72, 0x0A),
            new(0x00, 0x00)
        });

    public static readonly SamplePrograms OutputTest = new(
        "Output Test",
        2,
        """
                        load    RF,0        ;initialize to 0
                        load    R7,1        ;increase step
               NextChar:addi    RF,RF,R7    ;increase (is written to RF(=screen))
                        jmp     NextChar    ;repeat
        """,
        new Instruction[]
        {
            new(0x2F, 0x00),
            new(0x27, 0x01),
            new(0x5F, 0xF7),
            new(0xB0, 0x04)
        });

    public static readonly SamplePrograms Template = new(
        "Template",
        3,
        """
                        load    R2,0
                        jmp     Print       ;Print number in R2
               
                        org     60h
               Print:
               ; Put the number in R2 in the output window
               ; Changed registers: R0, RA, RB, RC, RE, RF
                        load    R0,39h      ;ASCII-code of '9'
                        load    RA,30h      ;ASCII-code of '0'
                        load    RB,7        ;'A'-'0'+10  (correction from numbers to letters)
                        ; determine high nibble
                        load    RC,0F0h     ;filter high nibble
                        and     RE,R2,RC    ;high nibble of R2 in RE
                        ror     RE,4        ;shift to low nibble
                        addi    RE,RE,RA    ;make it ASCII
                        jmpLE   RE<=R0,isNum1    ;is an ASCII number
                        addi    RE,RE,RB         ;correct to ASCII letter
               isNum1:  store   RE,[HiNibble]    ;put high nibble on its place
                        ; determine low nibble
                        load    RC,0Fh      ;filter low nibble
                        and     RE,R2,RC    ;low nibble of R2 in RE
                        addi    RE,RE,RA    ;make it ASCII
                        jmpLE   RE<=R0,isNum2    ;is an ASCII number
                        addi    RE,RE,RB         ;correct to ASCII letter
               isNum2:  store   RE,[LoNibble]    ;put low nibble on its place
                        ; print string
                        load    R0,0        ;string terminator
                        load    RA,Text
                        load    RB,1        ;index increaser
               NextChar:load    RF,[RA]     ;put character in output window
                        jmpEQ   RF=R0,Exit  ;when string-terminator, then exit
                        addi    RA,RA,RB    ;increase index
                        jmp     NextChar
               Exit:    halt
               
               Text:    db      10,"Answer : $"
               HiNibble:db      '?'
               LoNibble:db      '?'
                        db      0           ;string-terminator
               
                        org     0A0h        ;the row starts here
               Rij:     db      0Eh, 10h, 1Ah, 6, -5, 24h, 6
                        db      0
        """,
        new Instruction[]
        {
            new(0x22, 0x00),
            new(0xB0, 0x60),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x00, 0x00),
            new(0x20, 0x39),
            new(0x2A, 0x30),
            new(0x2B, 0x07),
            new(0x2C, 0xF0),
            new(0x8E, 0x2C),
            new(0xAE, 0x04),
            new(0x5E, 0xEA),
            new(0xFE, 0x72),
            new(0x5E, 0xEB),
            new(0x3E, 0x9B),
            new(0x2C, 0x0F),
            new(0x8E, 0x2C),
            new(0x5E, 0xEA),
            new(0xFE, 0x7E),
            new(0x5E, 0xEB),
            new(0x3E, 0x9C),
            new(0x20, 0x00),
            new(0x2A, 0x90),
            new(0x2B, 0x01),
            new(0xD0, 0xFA),
            new(0xBF, 0x8E),
            new(0x5A, 0xAB),
            new(0xB0, 0x86),
            new(0xC0, 0x00),
            new(0x0A, 0x41),
            new(0x6E, 0x73),
            new(0x77, 0x65),
            new(0x72, 0x20),
            new(0x3A, 0x20),
            new(0x24, 0x3F),
            new(0x3F, 0x00),
            new(0x00, 0x00),
            new(0x0E, 0x10),
            new(0x1A, 0x06),
            new(0xFB, 0x24),
            new(0x06, 0x00)
        });

    public string Code { get; private set; }
    public Instruction[] Instructions { get; private set; }

    private SamplePrograms(string name, int id, string code, Instruction[] instructions) : base(name, id)
    {
        Code = code;
        Instructions = instructions;
    }
}