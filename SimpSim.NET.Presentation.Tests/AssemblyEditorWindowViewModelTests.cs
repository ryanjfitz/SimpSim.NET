using System.Collections.Generic;
using System.Linq;
using SimpSim.NET.Presentation.ViewModels;
using Xunit;

namespace SimpSim.NET.Presentation.Tests
{
    public class AssemblyEditorWindowViewModelTests
    {
        [Fact(Skip = "Not passing due to use of async/await in AssembleCommand.")]
        public void AssembleCommandShouldAssembleInstructionsToMemory()
        {
            SimpleSimulator simulator = new SimpleSimulator();

            AssemblyEditorWindowViewModel viewModel = new AssemblyEditorWindowViewModel(simulator);

            viewModel.AssemblyEditorText = SamplePrograms.HelloWorldCode;

            viewModel.AssembleCommand.Execute(null);

            var expectedBytes = SamplePrograms.HelloWorldInstructions.SelectMany(instruction => instruction.Bytes).ToList();

            var actualBytes = new List<byte>();

            for (byte address = 0; address < expectedBytes.Count; address++)
                actualBytes.Add(simulator.Memory[address]);

            Assert.Equal(expectedBytes, actualBytes);
        }
    }
}
