using System.IO;
using System.Text.Json;

namespace SimpSim.NET
{
    public interface IStateSaver
    {
        void SaveMemory(Memory memory, FileInfo file);
        Memory LoadMemory(FileInfo file);
    }

    public class StateSaver : IStateSaver
    {
        public void SaveMemory(Memory memory, FileInfo file)
        {
            using (var streamWriter = file.CreateText())
                streamWriter.Write(JsonSerializer.Serialize(memory));
        }

        public Memory LoadMemory(FileInfo file)
        {
            using (var streamReader = file.OpenText())
                return JsonSerializer.Deserialize<Memory>(streamReader.ReadToEnd());
        }
    }
}
