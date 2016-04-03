using NUnit.Framework;
using SimpSim.NET.Presentation.ViewModels;

namespace SimpSim.NET.Presentation.Tests
{
    [TestFixture]
    public class SystemRegistersViewModelTests
    {
        [Test]
        public void ShouldBeAbleToResetProgramCounter()
        {
            SimpleSimulator simulator = new SimpleSimulator();

            simulator.Machine.ProgramCounter = 0xFF;

            SystemRegistersViewModel viewModel = new SystemRegistersViewModel(simulator);

            viewModel.ResetProgramCounterCommand.Execute(null);

            Assert.AreEqual(0x00, simulator.Machine.ProgramCounter);
        }
    }
}
