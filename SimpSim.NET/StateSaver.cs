using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpSim.NET
{
    public interface IStateSaver
    {
        Task SaveMemoryAsync(Memory memory, FileInfo file);
        Task<Memory> LoadMemoryAsync(FileInfo file);
    }

    public class StateSaver : IStateSaver
    {
        public async Task SaveMemoryAsync(Memory memory, FileInfo file)
        {
            using (var fileStream = file.Create())
            {
                byte[] bytes = memory.ToArray();
                await JsonSerializer.SerializeAsync(fileStream, bytes);
            }
        }

        public async Task<Memory> LoadMemoryAsync(FileInfo file)
        {
            using (var fileStream = file.OpenRead())
            {
                byte[] bytes = await JsonSerializer.DeserializeAsync<byte[]>(fileStream);
                Memory memory = new Memory();
                memory.LoadByteArray(bytes);
                return memory;
            }
        }
    }
}
