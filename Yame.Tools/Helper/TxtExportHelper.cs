using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yame.Tools.NetCore.Helper
{
    public class TxtExportHelper
    {
        /// <summary>
        /// 將 集合物件 轉成 TXT檔的 byte array (逗號分隔)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public static byte[] GetBytesForTxtFile<T>(List<T> data, Converter<T, string> converter)
        {
            if (data.Any())
            {
                var convertAll = data.ConvertAll(converter);
                string completeString = String.Join(",", convertAll);

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
