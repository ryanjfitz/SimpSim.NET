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
        public void SaveCommandShouldSaveMachineStateToFile()
        {
            FileInfo file = new FileInfo("machine_save.bin");

            _userInputService.Setup(s => s.GetSaveFileName()).Returns(file).Verifiable();

            _stateSaver.Setup(s => s.SaveMachine(_simulator.Machine, file)).Verifiable();

            _viewModel.SaveCommand.Execute(null);

            _userInputService.Verify();

            _stateSaver.Verify();
        }

        [Test]
        public void SaveCommandShouldNotSaveMachineStateToFileIfFileIsNull()
        {
            FileInfo file = null;

            _userInputService.Setup(s => s.GetSaveFileName()).Returns(file).Verifiable();

            _viewModel.SaveCommand.Execute(null);

            _userInputService.Verify();

            _stateSaver.Verify(s => s.SaveMachine(_simulator.Machine, file), Times.Never);
        }

        [Test]
        public void OpenCommandShouldLoadMachineStateFromFile()
        {
            FileInfo file = new FileInfo("machine_save.bin");

            _userInputService.Setup(s => s.GetOpenFileName()).Returns(file).Verifiable();

            _stateSaver.Setup(s => s.LoadMachine(file)).Verifiable();

            _viewModel.OpenCommand.Execute(null);

            _userInputService.Verify();

            _stateSaver.Verify();
        }

        [Test]
        public void OpenCommandShouldNotLoadMachineStateFromFileIfFileIsNull()
        {
            FileInfo file = null;

            _userInputService.Setup(s => s.GetOpenFileName()).Returns(file).Verifiable();

            _viewModel.OpenCommand.Execute(null);

            _userInputService.Verify();

            _stateSaver.Verify(s => s.LoadMachine(file), Times.Never);
        }
    }
}
