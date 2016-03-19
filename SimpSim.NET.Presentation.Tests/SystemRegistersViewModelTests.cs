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
            ModelSingletons.Machine.ProgramCounter = 0xFF;

            SystemRegistersViewModel viewModel = new SystemRegistersViewModel();
            viewModel.ResetProgramCounterCommand.Execute(null);

            Assert.AreEqual(0x00, ModelSingletons.Machine.ProgramCounter);
        }
    }
}
