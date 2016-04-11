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
        private Mock<IDialogService> _dialogService;
        private Mock<StateSaver> _stateSaver;
        private MachineControlsViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _simulator = new SimpleSimulator();
            _dialogService = new Mock<IDialogService>();
            _stateSaver = new Mock<StateSaver>();
            _viewModel = new MachineControlsViewModel(_simulator, _dialogService.Object, _stateSaver.Object);
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
        public void SaveCommandShouldSerializeMachineToSaveFile()
        {
            FileInfo saveFile = new FileInfo("machine_save.bin");

            _dialogService.Setup(s => s.GetSaveFileName()).Returns(saveFile).Verifiable();

            _stateSaver.Setup(s => s.SaveMachine(_simulator.Machine, saveFile)).Verifiable();

            _viewModel.SaveCommand.Execute(null);

            _dialogService.Verify();

            _stateSaver.Verify();
        }

        [Test]
        public void SaveCommandShouldNotSerializeMachineIfSaveFileIsNull()
        {
            FileInfo saveFile = null;

            _dialogService.Setup(s => s.GetSaveFileName()).Returns(saveFile).Verifiable();

            _viewModel.SaveCommand.Execute(null);

            _dialogService.Verify();

            _stateSaver.Verify(s => s.SaveMachine(_simulator.Machine, saveFile), Times.Never);
        }

        [Test]
        public void OpenCommandShouldDeserializeMachineFromFile()
        {
            FileInfo file = new FileInfo("machine_save.bin");

            _dialogService.Setup(s => s.GetOpenFileName()).Returns(file).Verifiable();

            _stateSaver.Setup(s => s.LoadMachine(file)).Verifiable();

            _viewModel.OpenCommand.Execute(null);

            _dialogService.Verify();

            _stateSaver.Verify();
        }

        [Test]
        public void OpenCommandShouldNotDeserializeMachineIfFileIsNull()
        {
            FileInfo file = null;

            _dialogService.Setup(s => s.GetOpenFileName()).Returns(file).Verifiable();

            _viewModel.OpenCommand.Execute(null);

            _dialogService.Verify();

            _stateSaver.Verify(s => s.LoadMachine(file), Times.Never);
        }
    }
}
