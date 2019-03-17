using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
