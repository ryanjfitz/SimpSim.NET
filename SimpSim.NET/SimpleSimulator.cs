namespace SimpSim.NET;

public class SimpleSimulator
{
    public readonly Memory Memory;

    public readonly Registers Registers;

    public readonly Machine Machine;

    public readonly Assembler Assembler;

    public SimpleSimulator()
    {
        Memory = new Memory();
        Registers = new Registers();
        Machine = new Machine(Memory, Registers);
        Assembler = new Assembler();
    }
}