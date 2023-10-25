using Xunit;

namespace SimpSim.NET.Tests;

public class ProgramTests
{
    private readonly Memory _memory;
    private string _actualOutput;
    private readonly Registers _registers;
    private readonly Machine _machine;

    public ProgramTests()
    {
        _memory = new Memory();

        _actualOutput = null;

        _registers = new Registers();
        _registers.ValueWrittenToOutputRegister += c => _actualOutput += c;

        _machine = new Machine(_memory, _registers);
    }

    [Fact]
    public async Task TestHelloWorldOutput()
    {
        _memory.LoadInstructions(SamplePrograms.HelloWorld.Instructions);

        await _machine.RunAsync();

        const string expectedOutput = "\nHello world !!\n    from the\n  Simple Simulator\n\0";

        Assert.Equal(expectedOutput, _actualOutput);
    }

    [Fact]
    public void TestAsciiOutput()
    {
        _memory.LoadInstructions(SamplePrograms.OutputTest.Instructions);

        string expectedOutput = string.Empty;
        for (int i = 0; i <= byte.MaxValue; i++)
            expectedOutput += (char)i;

        do
        {
            _machine.Step();
        } while (_actualOutput.Length < expectedOutput.Length);

        Assert.Equal(expectedOutput, _actualOutput);
    }

    [Fact]
    public async Task TestTemplateOutput()
    {
        _memory.LoadInstructions(SamplePrograms.Template.Instructions);

        await _machine.RunAsync();

        const string expectedOutput = "\nAnswer : $00\0";

        Assert.Equal(expectedOutput, _actualOutput);
    }
}