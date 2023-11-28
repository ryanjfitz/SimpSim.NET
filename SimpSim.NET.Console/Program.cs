using SimpSim.NET;

string assemblyCode = null;

switch (args.Length)
{
    case 0:
        do
        {
            Console.WriteLine("Choose a sample program to execute:\n");
            var menuItems = SamplePrograms.List.OrderBy(sp => sp.Value).Select(sp => $"{sp.Value}) {sp.Name}");
            Console.WriteLine(string.Join(Environment.NewLine, menuItems));

            if (int.TryParse(Console.ReadLine(), out int selection) && SamplePrograms.TryFromValue(selection, out var sampleProgram))
                assemblyCode = sampleProgram.Code;

            if (assemblyCode == null)
                Console.WriteLine("Invalid selection. Please choose again.\n");
        } while (assemblyCode == null);

        break;
    case 1:
        string file = args[0];
        try
        {
            assemblyCode = await File.ReadAllTextAsync(file);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unable to read file \"{file}\": {ex.Message}");
        }

        break;
    default:
        Console.WriteLine("Usage:");
        Console.WriteLine("  SimpSim.NET.Console [<assembly-file>]");
        break;
}

if (assemblyCode != null)
{
    SimpleSimulator simulator = new SimpleSimulator();

    Instruction[] instructions = simulator.Assembler.Assemble(assemblyCode);

    simulator.Memory.LoadInstructions(instructions);

    simulator.Registers.ValueWrittenToOutputRegister += Console.Write;

    await simulator.Machine.RunAsync();
}