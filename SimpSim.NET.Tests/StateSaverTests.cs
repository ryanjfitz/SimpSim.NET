using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace SimpSim.NET.Tests;

public class StateSaverTests
{
    private readonly StateSaver _stateSaver;

    public StateSaverTests()
    {
        _stateSaver = new StateSaver();
    }

    [Fact]
    public async Task ShouldBeAbleToSaveMemoryState()
    {
        FileInfo file = null;

        try
        {
            file = new FileInfo(Path.Combine(Path.GetTempPath(), "MemorySaveFile.prg"));

            Memory expectedMemory = new Memory();

            Random random = new Random();
            for (int address = 0; address <= byte.MaxValue; address++)
                expectedMemory[(byte)address] = (byte)random.Next(0x00, byte.MaxValue);

            await _stateSaver.SaveMemoryAsync(expectedMemory, file);

            Assert.True(file.Exists);

            Memory actualMemory = await _stateSaver.LoadMemoryAsync(file);

            for (int address = 0; address <= byte.MaxValue; address++)
                Assert.Equal(expectedMemory[(byte)address], actualMemory[(byte)address]);
        }
        finally
        {
            file?.Delete();
        }
    }
}