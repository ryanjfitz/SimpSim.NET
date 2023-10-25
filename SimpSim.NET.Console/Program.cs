namespace SimpSim.NET.Console;

class Program
{
    static async Task Main(string[] args)
    {
        string assemblyCode = null;

        switch (args.Length)
        {
            case 0:
                bool isValidSelection;

                do
                {
                    System.Console.WriteLine("Choose a sample program to execute:\n");
                    foreach (var sampleProgram in SamplePrograms.List.OrderBy(sp => sp.Value))
                        System.Console.WriteLine($"{sampleProgram.Value}) {sampleProgram.Name}");

                    isValidSelection = int.TryParse(System.Console.ReadLine(), out int selection);

                    if (SamplePrograms.TryFromValue(selection, out var result))
                        assemblyCode = result.Code;
                    else
                        isValidSelection = false;

                    if (!isValidSelection)
                        System.Console.WriteLine("Invalid selection. Please choose again.\n");
                } while (!isValidSelection);

                break;
            case 1:
                string file = args[0];
                try
                {
                    assemblyCode = await File.ReadAllTextAsync(file);
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
            await RunProgram(assemblyCode);
    }

    private static async Task RunProgram(string assemblyCode)
    {
        SimpleSimulator simulator = new SimpleSimulator();

        Instruction[] instructions = simulator.Assembler.Assemble(assemblyCode);

        simulator.Memory.LoadInstructions(instructions);

        simulator.Registers.ValueWrittenToOutputRegister += System.Console.Write;

        await simulator.Machine.RunAsync();
    }
}