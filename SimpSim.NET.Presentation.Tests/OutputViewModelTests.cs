using Moq;
using SimpSim.NET.Presentation.ViewModels;
using Xunit;

namespace SimpSim.NET.Presentation.Tests
{
    public class OutputViewModelTests
    {
        [Fact]
        public void ShouldUseWindowServiceToOpenAssemblyEditorWindow()
        {
            var mockWindowService = new Mock<IWindowService>();
            mockWindowService.Setup(m => m.ShowAssemblyEditorWindow(It.IsAny<string>())).Verifiable();

            OutputViewModel viewModel = new OutputViewModel(mockWindowService.Object, new SimpleSimulator());

            viewModel.OpenAssemblyEditorWindow.Execute(null);

            mockWindowService.Verify();
        }
    }
}
