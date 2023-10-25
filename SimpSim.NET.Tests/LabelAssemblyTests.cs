using System.Linq;
using SimpSim.NET.Assembly;
using Xunit;

namespace SimpSim.NET.Tests;

public class LabelAssemblyTests
{
    private readonly Assembler _assembler;

    public LabelAssemblyTests()
    {
        _assembler = new Assembler();
    }

    [Fact]
    public void ShouldValidateValidLabels()
    {
        _assembler.Assemble("Label:");

        _assembler.Assemble("NextChar2:");

        _assembler.Assemble("~MyLabel~:");
    }

    [Fact]
    public void ShouldNotValidateLabelsThatContainInvalidCharacters()
    {
        var allCharacters = Enumerable.Range(0, 256).Select(i => (char)i);

        var capitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        var numericCharacters = "0123456789";
        var specialCharacters = "#_~";
        var commentCharacters = ";";

        var invalidCharacters =
            allCharacters
                .Except(capitalLetters)
                .Except(lowercaseLetters)
                .Except(numericCharacters)
                .Except(specialCharacters)
                .Except(commentCharacters);

        foreach (char c in invalidCharacters)
            Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble(c + ":"));
    }

    [Fact]
    public void ShouldNotValidateLabelsThatStartWithNumber()
    {
        Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble("1Label:"));

        Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble("2NextChar2:"));

        Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble("31~MyLabel~:"));
    }

    [Fact]
    public void ShouldValidateIfNoLabel()
    {
        _assembler.Assemble("");
    }

    [Fact]
    public void ShouldNotValidateIfColonOnly()
    {
        Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble(":"));
        Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble("::"));
        Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble(":::"));
    }

    [Fact]
    public void ShouldNotAssembleUndefinedLabel()
    {
        Assert.Throws<LabelAssemblyException>(() => _assembler.Assemble("load R1,UndefinedLabel"));
    }
}