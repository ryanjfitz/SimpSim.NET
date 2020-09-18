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
            {
                byte[] bytes = memory.ToArray();
                string serializedBytes = JsonSerializer.Serialize(bytes);
                streamWriter.Write(serializedBytes);
            }
        }

        public Memory LoadMemory(FileInfo file)
        {
            using (var streamReader = file.OpenText())
            {
                string serializedBytes = streamReader.ReadToEnd();
                byte[] bytes = JsonSerializer.Deserialize<byte[]>(serializedBytes);
                return new Memory(bytes);
            }
        }
    }
}
