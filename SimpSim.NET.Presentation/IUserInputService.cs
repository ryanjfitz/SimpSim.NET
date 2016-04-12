using System.IO;

namespace SimpSim.NET.Presentation
{
    public interface IUserInputService
    {
        FileInfo GetOpenFileName();
        FileInfo GetSaveFileName();
    }
}
