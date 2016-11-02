using System;
using System.IO;

namespace SimpSim.NET.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string assemblyCode = null;

            switch (args.Length)
            {
                case 0:
                    bool isValidSelection;

                    do
                    {
                        System.Console.WriteLine("Choose a sample program to execute:\n");
                        System.Console.WriteLine("1) Hello World");
                        System.Console.WriteLine("2) Output Test");
                        System.Console.WriteLine("3) Template");

                        int selection;
                        isValidSelection = int.TryParse(System.Console.ReadLine(), out selection) && selection >= 1 && selection <= 3;

                        if (!isValidSelection)
                            System.Console.WriteLine("Invalid selection. Please choose again.\n");
                        else
                            switch (selection)
                            {
                                case 1:
                                    assemblyCode = SamplePrograms.HelloWorldCode;
                                    break;
                                case 2:
                                    assemblyCode = SamplePrograms.OutputTestCode;
                                    break;
                                case 3:
                                    assemblyCode = SamplePrograms.TemplateCode;
                                    break;
                            }
                    } while (!isValidSelection);
                    break;
                case 1:
                    string file = args[0];
                    try
                    {
                        assemblyCode = File.ReadAllText(file);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine($"Unable to read file \"{file}\": {ex.Message}");
                    }
                    break;
                default:
                    System.Console.WriteLine("Usage:");
                    System.Console.WriteLine("  SimpSim.NET.Console [<assembly-file>]");
                    break;
            }

            if (assemblyCode != null)
                RunProgram(assemblyCode);
        }

        private static void RunProgram(string assemblyCode)
        {
            Assembler assembler = new Assembler();

            Instruction[] instructions = assembler.Assemble(assemblyCode);

            Memory memory = new Memory();
            memory.LoadInstructions(instructions);

            Registers registers = new Registers();
            registers.ValueWrittenToOutputRegister += System.Console.Write;

            Machine machine = new Machine(memory, registers);
            machine.Run(25);

            System.Console.ReadLine();
        }
    }
}
