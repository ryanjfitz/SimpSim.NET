using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SimpSim.NET.Presentation.ViewModels;

namespace SimpSim.NET.Presentation.Tests
{
    [TestFixture]
    public class AssemblyEditorWindowViewModelTests
    {
        [Test]
        public void AssembleCommandShouldAssembleInstructionsToMemory()
        {
            AssemblyEditorWindowViewModel assemblyEditorWindowViewModel = new AssemblyEditorWindowViewModel(null);

            assemblyEditorWindowViewModel.AssemblyEditorText = SamplePrograms.HelloWorldCode;

            assemblyEditorWindowViewModel.AssembleCommand.Execute(null);

            var expectedBytes = SamplePrograms.HelloWorldInstructions.SelectMany(instruction => instruction.Bytes).ToList();

            var actualBytes = new List<byte>();

            for (byte address = 0; address < expectedBytes.Count; address++)
                actualBytes.Add(ModelSingletons.Memory[address]);

            CollectionAssert.AreEqual(expectedBytes, actualBytes);
        }
    }
}
