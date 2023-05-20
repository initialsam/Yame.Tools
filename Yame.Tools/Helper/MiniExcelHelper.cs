using MiniExcelLibs.OpenXml;
using MiniExcelLibs;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using MiniExcelLibs.Attributes;

namespace Yame.Tools.NetCore.Helper
{
    public static class MiniExcelHelper
    {
        /// <summary>
        /// 匯出Base64格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="isDefaultStyle">是否預設樣式</param>
        /// <param name="isAutoFilter">是否要篩選</param>
        /// <returns></returns>
        public static string ExportBase64<T>(IEnumerable<T> data, bool isDefaultStyle = true, bool isAutoFilter = true)
        {
            byte[] bytes = ConvertExcel(data, isDefaultStyle, isAutoFilter);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 匯出Byte[]格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="isDefaultStyle">是否預設樣式</param>
        /// <param name="isAutoFilter">是否要篩選</param>
        /// <returns></returns>
        public static byte[] ExportByteArray<T>(IEnumerable<T> data, bool isDefaultStyle = true, bool isAutoFilter = true)
        {
            return ConvertExcel(data, isDefaultStyle, isAutoFilter);
        }

        /// <summary>
        /// 轉出Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="isDefaultStyle">是否預設樣式</param>
        /// <param name="isAutoFilter">是否要篩選</param>
        /// <returns></returns>
        private static byte[] ConvertExcel<T>(IEnumerable<T> data, bool isDefaultStyle = true, bool isAutoFilter = true)
        {
            byte[] bytes = null;


            using (var stream = new MemoryStream())
            {
                var config = new OpenXmlConfiguration()
                {
                    TableStyles = TableStyles.Default,
                    AutoFilter = isAutoFilter,
                };
                if (!isDefaultStyle)
                    config.TableStyles = TableStyles.None;

                stream.SaveAs(value: data, excelType: ExcelType.XLSX, configuration: config);

                bytes = stream.ToArray();
            }
            return bytes;
        }
    }

    public class MemberRawData
    {
        [ExcelColumn(Name = "姓名", Index = 0)]
        public string Name { get; set; }
        [ExcelIgnore]
        public string Gender { get; set; }
        [ExcelColumn(Name = "建立時間", Index = 1, Format = "yyyy/MM/dd")]
        public DateTime CreateTime { get; set; }
    
        [ExcelIgnore]
        public bool IsDelete { get; set; }
        [ExcelColumn(Name = "狀態", Index = 2)]
        public string Status { get { return this.IsDelete ? "已刪除" : "使用中"; } }
      
    }
}
