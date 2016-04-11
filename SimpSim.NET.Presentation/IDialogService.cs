using System.IO;

namespace SimpSim.NET.Presentation
{
    public interface IDialogService
    {
        FileInfo GetOpenFileName();
        FileInfo GetSaveFileName();
    }
}
