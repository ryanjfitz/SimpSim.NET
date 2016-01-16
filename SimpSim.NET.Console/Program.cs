using System.Threading;

namespace SimpSim.NET.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProgram(SamplePrograms.HelloWorldCode);
            RunProgram(SamplePrograms.TemplateCode);
            RunProgram(SamplePrograms.OutputTestCode);
        }

        private static void RunProgram(string assemblyCode)
        {
            Assembler assembler = new Assembler();

            Instruction[] instructions = assembler.Assemble(assemblyCode);

            Memory memory = new Memory();
            memory.LoadInstructions(instructions);

            Registers registers = new Registers();
            registers.ValueWrittenToOutputRegister += WriteOutput;

            Machine machine = new Machine(memory, registers);
            machine.Run();

            System.Console.ReadLine();
        }

        private static void WriteOutput(char c)
        {
            System.Console.Write(c);
            Thread.Sleep(100);
        }
    }
}
