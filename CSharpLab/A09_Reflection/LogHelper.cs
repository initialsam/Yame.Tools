using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A09_Reflection
{
    public class LogHelper
    {
        /// <summary>
        /// 分隔字串預設 <BR/>
        /// </summary>
        private static string _delimiter = "<BR/>";

        /// <summary>
        /// 分隔字串預設 <BR/> ，例如 \r\n
        /// </summary>
        public static string Delimiter
        {
            get { return _delimiter; }
            set { _delimiter = value; }
        }

        /// <summary>
        /// 箭頭符號預設 ➜
        /// </summary>
        private static string _arrowSymbol = "➜";

        /// <summary>
        ///  箭頭符號預設 ➜ ，例如 =>，->
        /// </summary>
        public static string ArrowSymbol
        {
            get { return _arrowSymbol; }
            set { _arrowSymbol = value; }
        }

        // 請先不要往下看
        // 請先不要往下看


































































        /// <summary>
        /// 比較兩個物件 紀錄差異的內容 before after 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">修改前物件</param>
        /// <param name="to">修改後物件</param>
        /// <returns></returns>
        public static string DetailedCompare<T>(T before  , T after)
            where T : class, new()
        {
            if (before == null && after == null) return String.Empty;
            if (before == null) before = new T();
            if (after == null) after = new T();

            Type type = typeof(T);
            var variances = new List<string>();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                var targetProperty = type.GetProperty(property.Name);
                object beforeValue = targetProperty.GetValue(before, null) ?? string.Empty;
                object afterValue = targetProperty.GetValue(after, null) ?? string.Empty;
                if (beforeValue.Equals(afterValue)) continue;
                variances.Add($"{beforeValue} {_arrowSymbol} {afterValue}");
            }
            return string.Join(_delimiter, variances);
        }

    }
}
