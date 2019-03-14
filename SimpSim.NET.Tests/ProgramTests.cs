using Xunit;

namespace SimpSim.NET.Tests
{
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
        public void TestHelloWorldOutput()
        {
            _memory.LoadInstructions(SamplePrograms.HelloWorldInstructions);

            _machine.Run();

            const string expectedOutput = "\nHello world !!\n    from the\n  Simple Simulator\n\0";

            Assert.Equal(expectedOutput, _actualOutput);
        }

        [Fact]
        public void TestAsciiOutput()
        {
            _memory.LoadInstructions(SamplePrograms.OutputTestNonInfiniteInstructions);

            _machine.Run();

            string expectedOutput = null;
            for (int i = 0; i <= byte.MaxValue; i++)
                expectedOutput += (char)i;

            Assert.Equal(expectedOutput, _actualOutput);
        }

        [Fact]
        public void TestTemplateOutput()
        {
            _memory.LoadInstructions(SamplePrograms.TemplateInstructions);

            _machine.Run();

            const string expectedOutput = "\nAnswer : $00\0";

            Assert.Equal(expectedOutput, _actualOutput);
        }
    }
}
