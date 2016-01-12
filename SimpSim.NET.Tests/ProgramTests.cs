using NUnit.Framework;

namespace SimpSim.NET.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        private Memory _memory;
        private string _actualOutput;
        private Registers _registers;
        private Machine _machine;

        [SetUp]
        public void SetUp()
        {
            _memory = new Memory();

            _actualOutput = null;

            _registers = new Registers();
            _registers.ValueWrittenToOutputRegister += (c => _actualOutput += c);

            _machine = new Machine(_memory, _registers);
        }

        [Test]
        public void TestHelloWorldOutput()
        {
            _memory.LoadInstructions(SamplePrograms.HelloWorldInstructions);

            _machine.Run();

            const string expectedOutput = "\nHello world !!\n    from the\n  Simple Simulator\n\0";

            Assert.AreEqual(expectedOutput, _actualOutput);
        }

        [Test]
        public void TestAsciiOutput()
        {
            _memory.LoadInstructions(SamplePrograms.OutputTestNonInfiniteInstructions);

            _machine.Run();

            string expectedOutput = null;
            for (int i = 0; i <= byte.MaxValue; i++)
                expectedOutput += (char)i;

            Assert.AreEqual(expectedOutput, _actualOutput);
        }

        [Test]
        public void TestTemplateOutput()
        {
            _memory.LoadInstructions(SamplePrograms.TemplateInstructions);

            _machine.Run();

            const string expectedOutput = "\nAnswer : $00\0";

            Assert.AreEqual(expectedOutput, _actualOutput);
        }
    }
}
