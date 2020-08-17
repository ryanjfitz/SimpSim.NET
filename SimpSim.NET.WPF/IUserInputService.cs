using System.IO;

namespace SimpSim.NET.WPF
{
    public interface IUserInputService
    {
        FileInfo GetOpenFileName();
        FileInfo GetSaveFileName();
    }
}
