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

                        isValidSelection = int.TryParse(System.Console.ReadLine(), out int selection);

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
                            default:
                                isValidSelection = false;
                                break;
                        }

                        if (!isValidSelection)
                            System.Console.WriteLine("Invalid selection. Please choose again.\n");
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
            SimpleSimulator simulator = new SimpleSimulator();

            Instruction[] instructions = simulator.Assembler.Assemble(assemblyCode);

            simulator.Memory.LoadInstructions(instructions);

            simulator.Registers.ValueWrittenToOutputRegister += System.Console.Write;

            simulator.Machine.Run(25);

            System.Console.ReadLine();
        }
    }
}
