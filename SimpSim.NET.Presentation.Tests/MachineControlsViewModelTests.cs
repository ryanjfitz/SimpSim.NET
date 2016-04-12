using System;
using System.IO;
using Moq;
using NUnit.Framework;
using SimpSim.NET.Presentation.ViewModels;

namespace SimpSim.NET.Presentation.Tests
{
    [TestFixture]
    public class MachineControlsViewModelTests
    {
        private SimpleSimulator _simulator;
        private Mock<IUserInputService> _userInputService;
        private Mock<StateSaver> _stateSaver;
        private MachineControlsViewModel _viewModel;

        private readonly FileInfo _memorySaveFile = new FileInfo(Path.Combine(Path.GetTempPath(), "MemorySaveFile.bin"));

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            DeleteSaveFiles();
        }

        [SetUp]
        public void SetUp()
        {
            _simulator = new SimpleSimulator();
            _userInputService = new Mock<IUserInputService>();
            _stateSaver = new Mock<StateSaver>();
            _viewModel = new MachineControlsViewModel(_simulator, _userInputService.Object, _stateSaver.Object);
        }

        [Test]
        public void ClearMemoryCommandShouldClearMemory()
        {
            for (int i = 0; i <= byte.MaxValue; i++)
                _simulator.Memory[(byte)i] = 0xFF;

            _viewModel.ClearMemoryCommand.Execute(null);

            for (int i = 0; i <= byte.MaxValue; i++)
                Assert.AreEqual(0x00, _simulator.Memory[(byte)i]);
        }

        [Test]
        public void ClearRegistersCommandShouldClearRegisters()
        {
            for (byte b = 0; b <= 0x0F; b++)
                _simulator.Registers[b] = 0xFF;

            _viewModel.ClearRegistersCommand.Execute(null);

            for (byte b = 0; b <= 0x0F; b++)
                Assert.AreEqual(0x00, _simulator.Registers[b]);
        }

        [Test]
        public void SaveCommandShouldSaveMemoryStateToFile()
        {
            _userInputService.Setup(s => s.GetSaveFileName()).Returns(_memorySaveFile).Verifiable();

            _stateSaver.Setup(s => s.SaveMemory(_simulator.Memory, _memorySaveFile)).Verifiable();

            _viewModel.SaveCommand.Execute(null);

            _userInputService.Verify();

            _stateSaver.Verify();
        }

        [Test]
        public void SaveCommandShouldNotSaveMemoryStateToFileIfFileIsNull()
        {
            _userInputService.Setup(s => s.GetSaveFileName()).Returns<FileInfo>(null).Verifiable();

            _viewModel.SaveCommand.Execute(null);

            _userInputService.Verify();

            _stateSaver.Verify(s => s.SaveMemory(It.IsAny<Memory>(), It.IsAny<FileInfo>()), Times.Never);
        }

        [Test]
        public void OpenCommandShouldLoadMemoryStateFromFile()
        {
            Memory expectedMemory = new Memory();

            Random random = new Random();
            for (int address = 0; address <= byte.MaxValue; address++)
                expectedMemory[(byte)address] = (byte)random.Next(0x00, byte.MaxValue);

            new StateSaver().SaveMemory(expectedMemory, _memorySaveFile);

            _userInputService.Setup(s => s.GetOpenFileName()).Returns(_memorySaveFile).Verifiable();

            _stateSaver.Setup(s => s.LoadMemory(_memorySaveFile)).CallBase().Verifiable();

            _viewModel.OpenCommand.Execute(null);

            _userInputService.Verify();

            _stateSaver.Verify();

            for (int i = 0; i < byte.MaxValue; i++)
                Assert.AreEqual(expectedMemory[(byte)i], _simulator.Memory[(byte)i]);
        }

        [Test]
        public void OpenCommandShouldNotLoadMemoryStateFromFileIfFileIsNull()
        {
            _userInputService.Setup(s => s.GetOpenFileName()).Returns<FileInfo>(null).Verifiable();

            _viewModel.OpenCommand.Execute(null);

            _userInputService.Verify();

            _stateSaver.Verify(s => s.LoadMemory(It.IsAny<FileInfo>()), Times.Never);
        }

        private void DeleteSaveFiles()
        {
            _memorySaveFile.Delete();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DeleteSaveFiles();
        }
    }
}
