using System;
using System.IO;
using Xunit;

namespace SimpSim.NET.Tests
{
    public class StateSaverTests
    {
        private readonly StateSaver _stateSaver;

        public StateSaverTests()
        {
            _stateSaver = new StateSaver();
        }

        [Fact]
        public void ShouldBeAbleToSaveMemoryState()
        {
            RunFileTest("MemorySaveFile.prg", file =>
            {
                Memory expectedMemory = new Memory();

                Random random = new Random();
                for (int address = 0; address <= byte.MaxValue; address++)
                    expectedMemory[(byte)address] = (byte)random.Next(0x00, byte.MaxValue);

                _stateSaver.SaveMemory(expectedMemory, file);

                Assert.True(file.Exists);

                Memory actualMemory = _stateSaver.LoadMemory(file);

                for (int address = 0; address <= byte.MaxValue; address++)
                    Assert.Equal(expectedMemory[(byte)address], actualMemory[(byte)address]);
            });
        }

        private void RunFileTest(string fileName, Action<FileInfo> testAction)
        {
            FileInfo file = null;

            try
            {
                file = new FileInfo(Path.Combine(Path.GetTempPath(), fileName));

                testAction(file);
            }
            finally
            {
                file?.Delete();
            }
        }
    }
}
