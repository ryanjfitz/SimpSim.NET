using NUnit.Framework;
using SimpSim.NET.Presentation.ViewModels;

namespace SimpSim.NET.Presentation.Tests
{
    [TestFixture, Ignore("Fails if " + nameof(AssemblyEditorWindowViewModelTests) + "executes first. Ignoring until a resolution is in place.")]
    public class MachineControlsViewModelTests
    {
        [Test]
        public void ClearMemoryCommandShouldClearMemory()
        {
            MachineControlsViewModel viewModel = new MachineControlsViewModel();

            for (int i = 0; i <= byte.MaxValue; i++)
                ModelSingletons.Memory[(byte)i] = 0xFF;

            viewModel.ClearMemoryCommand.Execute(null);

            for (int i = 0; i <= byte.MaxValue; i++)
                Assert.AreEqual(0x00, ModelSingletons.Memory[(byte)i]);
        }

        [Test]
        public void ClearRegistersCommandShouldClearRegisters()
        {
            MachineControlsViewModel viewModel = new MachineControlsViewModel();

            for (byte b = 0; b <= 0x0F; b++)
                ModelSingletons.Registers[b] = 0xFF;

            viewModel.ClearRegistersCommand.Execute(null);

            for (byte b = 0; b <= 0x0F; b++)
                Assert.AreEqual(0x00, ModelSingletons.Registers[b]);
        }
    }
}
