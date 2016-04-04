using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SimpSim.NET
{
    public class StateSaver
    {
        public void SaveMemory(Memory memory, string fileName)
        {
            Save(memory, fileName);
        }

        public Memory LoadMemory(string saveFile)
        {
            return Load<Memory>(saveFile);
        }

        public void SaveRegisters(Registers registers, string fileName)
        {
            Save(registers, fileName);
        }

        public Registers LoadRegisters(string saveFile)
        {
            return Load<Registers>(saveFile);
        }

        public void SaveMachine(Machine machine, string fileName)
        {
            Save(machine, fileName);
        }

        public Machine LoadMachine(string saveFile)
        {
            return Load<Machine>(saveFile);
        }

        private void Save(object @object, string fileName)
        {
            using (var fileStream = File.Create(fileName))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, @object);
            }
        }

        private T Load<T>(string saveFile)
        {
            using (var fileStream = File.OpenRead(saveFile))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return (T)binaryFormatter.Deserialize(fileStream);
            }
        }
    }
}
