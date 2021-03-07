using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;

namespace Yame.Tools.Helper
{
    public class FileHelper
    {
        /// <summary>
        /// 讀取檔案內容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string FileReader(string path)
        {
            StreamReader sr;
            try
            {
                sr = new StreamReader(path);
                string res = sr.ReadToEnd();
                sr.Close();
                return res;
            }
            catch
            {
                return "";
            }
        }
    }

}
