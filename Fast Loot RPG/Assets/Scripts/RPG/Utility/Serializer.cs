using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RPG.Utility
{
    public static class Serializer
    {
        public static bool SerializeToFile(object data, string path)
        {
            FileStream stream = File.Create(path); 
            BinaryFormatter formatter = new BinaryFormatter();
            
            formatter.Serialize(stream, data);
            stream.Close();
            
            return true;
        }

        public static T DeserializeFromFile<T>(string path)
        {
            FileStream stream = File.OpenRead(path);
            BinaryFormatter formatter = new BinaryFormatter();
            
            T data = (T)formatter.Deserialize(stream);
            stream.Close();
            
            return data;
        }
    }
}