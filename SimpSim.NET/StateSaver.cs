using System.IO;
using System.Text.Json;

namespace SimpSim.NET
{
    public class StateSaver
    {
        public virtual void SaveMemory(Memory memory, FileInfo file)
        {
            Save(memory, file);
        }

        public virtual Memory LoadMemory(FileInfo file)
        {
            return Load<Memory>(file);
        }

        public virtual void SaveRegisters(Registers registers, FileInfo file)
        {
            Save(registers, file);
        }

        public virtual Registers LoadRegisters(FileInfo file)
        {
            return Load<Registers>(file);
        }

        public virtual void SaveMachine(Machine machine, FileInfo file)
        {
            Save(machine, file);
        }

        public virtual Machine LoadMachine(FileInfo file)
        {
            return Load<Machine>(file);
        }

        private void Save(object @object, FileInfo file)
        {
            using (var streamWriter = file.CreateText())
                streamWriter.Write(JsonSerializer.Serialize(@object));
        }

        private T Load<T>(FileInfo file)
        {
            using (var streamReader = file.OpenText())
                return JsonSerializer.Deserialize<T>(streamReader.ReadToEnd());
        }
    }
}
