using NUnit.Framework;
using SimpSim.NET.Presentation.ViewModels;

namespace SimpSim.NET.Presentation.Tests
{
    [TestFixture]
    public class MachineControlsViewModelTests
    {
        private SimpleSimulator _simulator;

        [SetUp]
        public void SetUp()
        {
            _simulator = new SimpleSimulator();
        }

        [Test]
        public void ClearMemoryCommandShouldClearMemory()
        {
            MachineControlsViewModel viewModel = new MachineControlsViewModel(_simulator);

            for (int i = 0; i <= byte.MaxValue; i++)
                _simulator.Memory[(byte)i] = 0xFF;

            viewModel.ClearMemoryCommand.Execute(null);

            for (int i = 0; i <= byte.MaxValue; i++)
                Assert.AreEqual(0x00, _simulator.Memory[(byte)i]);
        }

        [Test]
        public void ClearRegistersCommandShouldClearRegisters()
        {
            MachineControlsViewModel viewModel = new MachineControlsViewModel(_simulator);

            for (byte b = 0; b <= 0x0F; b++)
                _simulator.Registers[b] = 0xFF;

            viewModel.ClearRegistersCommand.Execute(null);

            for (byte b = 0; b <= 0x0F; b++)
                Assert.AreEqual(0x00, _simulator.Registers[b]);
        }
    }
}
