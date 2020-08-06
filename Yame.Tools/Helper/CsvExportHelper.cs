using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yame.Tools.NetCore.Helper
{
    public class CsvExportHelper
    {
        /// <summary>
        /// 將 集合物件 轉成 CSV檔的 byte array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <param name="converter"></param>
        /// <param name="headerrow"></param>
        /// <returns></returns>
        public static byte[] GetBytesForCSVFile<T>(List<T> data, Converter<T, string> converter, string headerrow = "")
        {
            if (data.Any())
            {
                var convertAll = data.ConvertAll(converter);

                convertAll.Insert(0, headerrow + Environment.NewLine);

                string completeString = String.Concat(convertAll);

                var bytes = Encoding.UTF8.GetBytes(completeString);
                var result = Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();

                return result;
            }
            else
            {
                return Array.Empty<byte>();
            }
        }
    }
}
