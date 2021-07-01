using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;

namespace AssignmentProject.Entities.Helpers
{
    public static class DataParser
    {
        public static JObject ParseToJsonObject(string json)
        {
            return JObject.Parse(json);
        }

        public static string ParseToXml<T>(T obj)
        {
            var xmlString = string.Empty;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using(var stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                {
                    xmlSerializer.Serialize(xmlWriter, obj);
                    xmlString = stringWriter.ToString();
                }
            }

            return xmlString;
        }
    }
}
