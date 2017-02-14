using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XFramework.Utilities
{
    public class XmlHelper
    {
        /// <summary>
        /// deserialize an object from a file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns>
        /// </returns>
        public static T LoadFromXml<T>(string fileName) where T : class
        {
            FileStream fs = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                return (T)serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// serialize an object to a file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        public static void SaveToXml<T>(string fileName, T data) where T : class
        {
            FileStream fs = null;

            try
            {
                var serializer = new XmlSerializer(typeof(T));
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                serializer.Serialize(fs, data);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// XML & Datacontract Serialize & Deserialize Helper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serialObject"></param>
        /// <param name="prefix"></param>
        /// <param name="nameSpace"></param>
        /// <param name="omitXmlDeclaration"></param>
        /// <returns></returns>
        public static string XmlSerializer<T>(T serialObject, string prefix = "", string nameSpace = "",
            bool omitXmlDeclaration = false) where T : class
        {
            string serialize;
            using (var output = new MemoryStream())
            {
                var setting = new XmlWriterSettings
                {
                    Encoding = new UTF8Encoding(false),
                    Indent = false,
                    OmitXmlDeclaration = omitXmlDeclaration
                };

                using (var writer = XmlWriter.Create(output, setting))
                {
                    var namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(prefix, nameSpace);
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, serialObject, namespaces);
                    serialize = Encoding.UTF8.GetString(output.ToArray());
                }
            }
            return serialize;
        }

        public static T XmlDeserialize<T>(string str) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            var reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(str)), Encoding.UTF8);
            return serializer.Deserialize(reader) as T;
        }

        //public static T DataContractDeserializer<T>(string xmlData) where T : class
        //{
        //    var stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlData));
        //    var reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());
        //    var ser = new DataContractSerializer(typeof(T));
        //    var deserializedPerson = (T)ser.ReadObject(reader, true);
        //    reader.Close();
        //    stream.Close();
        //    return deserializedPerson;
        //}

        //public static string DataContractSerializer<T>(T myObject) where T : class
        //{
        //    var stream = new MemoryStream();
        //    var ser = new DataContractSerializer(typeof(T));
        //    ser.WriteObject(stream, myObject);
        //    stream.Close();
        //    return Encoding.UTF8.GetString(stream.ToArray());
        //}
    }
}
