using Moq;
using NUnit.Framework;
using SimpSim.NET.Presentation.ViewModels;

namespace SimpSim.NET.Presentation.Tests
{
    [TestFixture]
    public class OutputViewModelTests
    {
        [Test]
        public void ShouldUseWindowServiceToOpenAssemblyEditorWindow()
        {
            var mockWindowService = new Mock<IWindowService>();
            mockWindowService.Setup(m => m.ShowAssemblyEditorWindow(It.IsAny<AssemblyEditorWindowViewModel>())).Verifiable();

            OutputViewModel viewModel = new OutputViewModel(mockWindowService.Object, new SimpleSimulator());

            viewModel.OpenAssemblyEditorWindow.Execute(null);

            mockWindowService.Verify();
        }
    }
}
