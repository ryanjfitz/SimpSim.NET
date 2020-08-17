using System.IO;
using Moq;
using SimpSim.NET.WPF;
using SimpSim.NET.WPF.ViewModels;
using Xunit;

namespace SimpSim.NET.Presentation.Tests
{
    public class MachineControlsViewModelTests
    {
        private readonly SimpleSimulator _simulator;
        private readonly Mock<IUserInputService> _userInputService;
        private readonly Mock<IWindowService> _windowService;
        private readonly Mock<StateSaver> _stateSaver;
        private readonly MachineControlsViewModel _viewModel;

        public MachineControlsViewModelTests()
        {
            _simulator = new SimpleSimulator();
            _userInputService = new Mock<IUserInputService>();
            _windowService = new Mock<IWindowService>();
            _stateSaver = new Mock<StateSaver>();
            _viewModel = new MachineControlsViewModel(_simulator, _userInputService.Object, _windowService.Object, _stateSaver.Object);
        }

        [Fact]
        public void ClearMemoryCommandShouldClearMemory()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
                _simulator.Memory[(byte)i] = 0xFF;

            _viewModel.ClearMemoryCommand.Execute(null);

            for (int i = 0; i <= byte.MaxValue; i++)
                Assert.Equal(0x00, _simulator.Memory[(byte)i]);
        }

        [Fact]
        public void ClearRegistersCommandShouldClearRegisters()
        {
            for (byte b = 0; b <= 0x0F; b++)
                _simulator.Registers[b] = 0xFF;

            _viewModel.ClearRegistersCommand.Execute(null);

            for (byte b = 0; b <= 0x0F; b++)
                Assert.Equal(0x00, _simulator.Registers[b]);
        }

        [Fact]
        public void SaveCommandShouldSaveMemoryStateToFile()
        {
            FileInfo memorySaveFile = new FileInfo("MemorySaveFile.prg");

            _userInputService.Setup(s => s.GetSaveFileName()).Returns(memorySaveFile).Verifiable();

            _stateSaver.Setup(s => s.SaveMemory(_simulator.Memory, memorySaveFile)).Verifiable();

            _viewModel.SaveCommand.Execute(null);

            _userInputService.Verify();

            _stateSaver.Verify();
        }

        [Fact]
        public void SaveCommandShouldNotSaveMemoryStateToFileIfFileIsNull()
        {
            _userInputService.Setup(s => s.GetSaveFileName()).Returns<FileInfo>(null).Verifiable();

            _viewModel.SaveCommand.Execute(null);

            _userInputService.Verify();

            _stateSaver.Verify(s => s.SaveMemory(It.IsAny<Memory>(), It.IsAny<FileInfo>()), Times.Never);
        }

        [Fact]
        public void NewCommandShouldUseOpenEmptyAssemblyEditorWindow()
        {
            _viewModel.NewCommand.Execute(null);

            _windowService.Verify(w => w.ShowAssemblyEditorWindow(null));
        }

        [Fact]
        public void OpenCommandShouldLoadMemoryStateFromMemoryFile()
        {
            FileInfo memorySaveFile = new FileInfo("MemorySaveFile.prg");

            _userInputService.Setup(s => s.GetOpenFileName()).Returns(memorySaveFile).Verifiable();

            _stateSaver.Setup(s => s.LoadMemory(memorySaveFile)).Returns(new Memory()).Verifiable();

            _viewModel.OpenCommand.Execute(null);

            _userInputService.Verify();

            _stateSaver.Verify();
        }

        [Fact]
        public void OpenCommandShouldNotLoadMemoryStateFromMemoryFileIfFileIsNull()
        {
            _userInputService.Setup(s => s.GetOpenFileName()).Returns<FileInfo>(null).Verifiable();

            _viewModel.OpenCommand.Execute(null);

            _userInputService.Verify();

            _stateSaver.Verify(s => s.LoadMemory(It.IsAny<FileInfo>()), Times.Never);
        }

        [Fact]
        public void OpenCommandShouldOpenAssemblyEditorWindowWithAssemblyFileContents()
        {
            FileInfo assemblyFile = new FileInfo("AssemblyFile.asm");

            try
            {
                File.WriteAllText(assemblyFile.FullName, "some text");

                _userInputService.Setup(s => s.GetOpenFileName()).Returns(assemblyFile).Verifiable();

                _viewModel.OpenCommand.Execute(null);

                _userInputService.Verify();

                _windowService.Verify(w => w.ShowAssemblyEditorWindow("some text"));
            }
            finally
            {
                assemblyFile.Delete();
            }
        }
    }
}