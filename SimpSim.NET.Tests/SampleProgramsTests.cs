using Xunit;

namespace SimpSim.NET.Tests;

public class SampleProgramsTests
{
    [Fact]
    public void Sample_programs_should_have_consecutive_values_starting_with_1()
    {
        var expected = Enumerable.Range(1, SamplePrograms.List.Count);

        var actual = SamplePrograms.List.Select(sp => sp.Value);

        Assert.Equal(expected, actual);
    }
}