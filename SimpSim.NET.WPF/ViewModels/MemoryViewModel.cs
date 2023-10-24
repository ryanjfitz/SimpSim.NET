using System.Runtime.CompilerServices;
using Prism.Mvvm;

namespace SimpSim.NET.WPF.ViewModels;

public class MemoryViewModel : BindableBase
{
    private const string IndexerName = "Addresses";

    private readonly SimpleSimulator _simulator;

    public MemoryViewModel(SimpleSimulator simulator)
    {
        _simulator = simulator;

        _simulator.Memory.Changed += () => RaisePropertyChanged(IndexerName);
    }

    [IndexerName(IndexerName)]
    public byte this[byte address]
    {
        get => _simulator.Memory[address];
        set => _simulator.Memory[address] = value;
    }
}