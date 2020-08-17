using SimpSim.NET.WPF.ViewModels;
using Xunit;

namespace SimpSim.NET.Presentation.Tests
{
    public class SystemRegistersViewModelTests
    {
        [Fact]
        public void ShouldBeAbleToResetProgramCounter()
        {
            SimpleSimulator simulator = new SimpleSimulator();

            simulator.Machine.ProgramCounter = 0xFF;

            SystemRegistersViewModel viewModel = new SystemRegistersViewModel(simulator);

            viewModel.ResetProgramCounterCommand.Execute(null);

            Assert.Equal(0x00, simulator.Machine.ProgramCounter);
        }
    }
}
