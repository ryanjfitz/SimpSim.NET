﻿using SimpSim.NET.WPF.ViewModels;
using Xunit;

namespace SimpSim.NET.WPF.Tests;

public class AssemblyEditorDialogViewModelTests
{
    [Fact]
    public async Task AssembleCommandShouldAssembleInstructionsToMemory()
    {
        SimpleSimulator simulator = new SimpleSimulator();

        AssemblyEditorDialogViewModel viewModel = new AssemblyEditorDialogViewModel(simulator);

        viewModel.AssemblyEditorText = SamplePrograms.HelloWorld.Code;

        await viewModel.Assemble(simulator);

        var expectedBytes = SamplePrograms.HelloWorld.Instructions.SelectMany(instruction => instruction.Bytes).ToList();

        var actualBytes = new List<byte>();

        for (byte address = 0; address < expectedBytes.Count; address++)
            actualBytes.Add(simulator.Memory[address]);

        Assert.Equal(expectedBytes, actualBytes);
    }
}