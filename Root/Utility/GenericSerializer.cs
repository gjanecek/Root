using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Utility
{
    public static class GenericSerializer
    {
        public static void Serialize<T>(T dataToSerialize, string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Default);
                writer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, dataToSerialize);
                writer.Close();
            }
        }
        public static T DeserializeFilePath<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T serializedData;
            using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                serializedData = (T)serializer.Deserialize(stream);
            }
            return serializedData;
        }

        public static T DeserializeXmlString<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T serializedData;
            using (var reader = new StringReader(xml))
            {
                serializedData = (T)serializer.Deserialize(reader);
            }
            return serializedData;
        }

    }
}
