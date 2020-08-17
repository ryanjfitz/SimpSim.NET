using SimpSim.NET.WPF.ViewModels;
using Xunit;

namespace SimpSim.NET.WPF.Tests
{
    public class OutputViewModelTests
    {
        [Fact]
        public void ClearCommandShouldEmptyOutputWindow()
        {
            OutputViewModel viewModel = new OutputViewModel(new SimpleSimulator());

            viewModel.OutputWindowText = "This is some output text.";
            Assert.NotNull(viewModel.OutputWindowText);

            viewModel.ClearCommand.Execute(null);
            Assert.Null(viewModel.OutputWindowText);
        }
    }
}
