using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.NetCore.Helper
{
    public static class LogHelper
    {
        /// <summary>
        /// 比較兩個物件 紀錄差異的內容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">修改前物件</param>
        /// <param name="to">修改後物件</param>
        /// <param name="ignore">忽略的屬性</param>
        /// <returns></returns>
        public static string DetailedCompare<T>(this T self, T to, params string[] ignore)
            where T : class
        {
            return DetailedCompare<T, int>(self, to, String.Empty, null, ignore);
        }

        /// <summary>
        ///  比較兩個物件 紀錄差異的內容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">修改前物件</param>
        /// <param name="to">修改後物件</param>
        /// <param name="master">先加入此字串到差異內容</param>
        /// <param name="ignore">忽略的屬性</param>
        /// <returns></returns>
        public static string DetailedCompareWithTitle<T>(this T self, T to, string master, params string[] ignore)
            where T : class
        {
            return DetailedCompare<T, int>(self, to, master, null, ignore);
        }

        /// <summary>
        /// 比較兩個物件 紀錄差異的內容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="self">修改前物件</param>
        /// <param name="to">修改後物件</param>
        /// <param name="specialText">特殊屬性要轉換才能拿到內容</param>
        /// <param name="ignore">忽略的屬性</param>
        /// <returns></returns>
        public static string DetailedCompare<T, U>(this T self, T to, Dictionary<string, Func<U, string>> specialText, params string[] ignore)
            where T : class
        {
            return DetailedCompare<T, U>(self, to, String.Empty, specialText, ignore);
        }

        /// <summary>
        /// 比較兩個物件 紀錄差異的內容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="self">修改前物件</param>
        /// <param name="to">修改後物件</param>
        /// <param name="master">先加入此字串到差異內容</param>
        /// <param name="specialText">特殊屬性要轉換才能拿到內容</param>
        /// <param name="ignore">忽略的屬性</param>
        /// <returns></returns>
        public static string DetailedCompare<T, U>(T self, T to, string master, Dictionary<string, Func<U, string>> specialText = null, params string[] ignore)
            where T : class
        {
            if (self == null && to == null) return String.Empty;
            if (specialText == null) specialText = new Dictionary<string, Func<U, string>>();

            Type type = typeof(T);

            var ignoreList = new List<string>(ignore);
            var variances = new List<string>();
            if (!String.IsNullOrWhiteSpace(master))
            {
                variances.Add(master);
            }
            var properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (System.Reflection.PropertyInfo pi in properties)
            {
                if (!ignoreList.Contains(pi.Name))
                {
                    object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                    object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                    if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                    {
                        if (specialText.ContainsKey(pi.Name))
                        {
                            selfValue = specialText[pi.Name]((U)selfValue);
                            toValue = specialText[pi.Name]((U)toValue);
                        }
                        if (selfValue == null) selfValue = string.Empty;

                        if (!selfValue.Equals(toValue))
                            variances.Add($"{selfValue} ➜ {toValue}");
                    }
                }
            }
            return string.Join("<BR/>", variances);


        }

        /// <summary>
        /// 新增的物件 紀錄內容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">新增的物件</param>
        /// <param name="ignore">忽略的屬性</param>
        /// <returns></returns>
        public static string DetailedCreate<T>(this T self, params string[] ignore)
            where T : class
        {
            return DetailedCreate<T, int>(self, null, ignore);
        }

        /// <summary>
        /// 新增的物件 紀錄內容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="self">新增的物件</param>
        /// <param name="specialText">特殊屬性要轉換才能拿到內容</param>
        /// <param name="ignore">忽略的屬性</param>
        /// <returns></returns>
        public static string DetailedCreate<T, U>(this T self, Dictionary<string, Func<U, string>> specialText, params string[] ignore)
            where T : class
        {
            if (self == null) return String.Empty;
            if (specialText == null) specialText = new Dictionary<string, Func<U, string>>();

            Type type = typeof(T);

            var ignoreList = new List<string>(ignore);
            var variances = new List<string>();
            var properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (System.Reflection.PropertyInfo pi in properties)
            {
                if (!ignoreList.Contains(pi.Name))
                {
                    object selfValue = type.GetProperty(pi.Name).GetValue(self, null);

                    if (specialText.ContainsKey(pi.Name))
                    {
                        selfValue = specialText[pi.Name]((U)selfValue);
                    }

                    variances.Add($"{selfValue}");

                }
            }
            return string.Join("<BR/>", variances);


        }

        /// <summary>
        /// 將傳入的內容用BR組合起來成一個字串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Join(params string[] data)
        {
            return string.Join("<BR/>", data);
        }

    }
}
