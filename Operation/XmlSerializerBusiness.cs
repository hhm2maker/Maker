using System;
using System.IO;
using System.Xml.Serialization;

namespace Operation
{
    public static class XmlSerializerBusiness
    {
        public static void Load<T>(ref T obj, String filePath) where T : class
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open)) {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                obj = serializer.Deserialize(stream) as T;
            }
        }

        public static void Load<T>(ref T obj, Stream filePath) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            obj = serializer.Deserialize(filePath) as T;
        }

        public static void Save(Object obj,String filePath)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, obj);
            }
        }
    }
}
