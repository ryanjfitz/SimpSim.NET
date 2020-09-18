﻿using System.IO;
using System.Threading.Tasks;
using Moq;
using SimpSim.NET.WPF.ViewModels;
using Xunit;

namespace SimpSim.NET.WPF.Tests
{
    public class MachineControlsViewModelTests
    {
        private readonly SimpleSimulator _simulator;
        private readonly Mock<IUserInputService> _userInputService;
        private readonly Mock<IDialogServiceAdapter> _dialogServiceAdapter;
        private readonly Mock<IStateSaver> _stateSaver;
        private readonly MachineControlsViewModel _viewModel;

        public MachineControlsViewModelTests()
        {
            _simulator = new SimpleSimulator();
            _userInputService = new Mock<IUserInputService>();
            _dialogServiceAdapter = new Mock<IDialogServiceAdapter>();
            _stateSaver = new Mock<IStateSaver>();
            _viewModel = new MachineControlsViewModel(_simulator, _userInputService.Object, _dialogServiceAdapter.Object, _stateSaver.Object);
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

            _stateSaver.Setup(s => s.SaveMemoryAsync(_simulator.Memory, memorySaveFile)).Verifiable();

            _viewModel.SaveCommand.Execute();

            _userInputService.Verify();

            _stateSaver.Verify();
        }

        [Fact]
        public void SaveCommandShouldNotSaveMemoryStateToFileIfFileIsNull()
        {
            _userInputService.Setup(s => s.GetSaveFileName()).Returns<FileInfo>(null).Verifiable();

            _viewModel.SaveCommand.Execute();

            _userInputService.Verify();

            _stateSaver.Verify(s => s.SaveMemoryAsync(It.IsAny<Memory>(), It.IsAny<FileInfo>()), Times.Never);
        }

        [Fact]
        public void NewCommandShouldUseOpenEmptyAssemblyEditorWindow()
        {
            _viewModel.NewCommand.Execute();

            _dialogServiceAdapter.Verify(w => w.ShowAssemblyEditorDialog(null));
        }

        [Fact]
        public void OpenCommandShouldLoadMemoryStateFromMemoryFile()
        {
            FileInfo memorySaveFile = new FileInfo("MemorySaveFile.prg");

            _userInputService.Setup(s => s.GetOpenFileName()).Returns(memorySaveFile).Verifiable();

            _stateSaver.Setup(s => s.LoadMemoryAsync(memorySaveFile)).Returns(Task.FromResult(new Memory())).Verifiable();

            _viewModel.OpenCommand.Execute();

            _userInputService.Verify();

            _stateSaver.Verify();
        }

        [Fact]
        public void OpenCommandShouldNotLoadMemoryStateFromMemoryFileIfFileIsNull()
        {
            _userInputService.Setup(s => s.GetOpenFileName()).Returns<FileInfo>(null).Verifiable();

            _viewModel.OpenCommand.Execute();

            _userInputService.Verify();

            _stateSaver.Verify(s => s.LoadMemoryAsync(It.IsAny<FileInfo>()), Times.Never);
        }

        [Fact]
        public void OpenCommandShouldOpenAssemblyEditorWindowWithAssemblyFileContents()
        {
            FileInfo assemblyFile = new FileInfo("AssemblyFile.asm");

            try
            {
                File.WriteAllText(assemblyFile.FullName, "some text");

                _userInputService.Setup(s => s.GetOpenFileName()).Returns(assemblyFile).Verifiable();

                _viewModel.OpenCommand.Execute();

                _userInputService.Verify();

                _dialogServiceAdapter.Verify(w => w.ShowAssemblyEditorDialog("some text"));
            }
            finally
            {
                assemblyFile.Delete();
            }
        }
    }
}