using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;

namespace Yame.Tools.Helper
{
    public static class XmlHelper
    {

        /// <summary>
        /// XML轉物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlContent) where T : class, new()
        {
            var result = new T();
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(T), new XmlRootAttribute("doc"));
                using (TextReader reader = new StringReader(xmlContent))
                {
                    result = deserializer.Deserialize(reader) as T;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"XML轉物件 發生錯誤 {ex}");
                return null;
            }


            return result;

        }
    }

}
