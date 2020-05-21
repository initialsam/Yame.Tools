using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 將ControllerName 移除最後的Controller字串
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static String GetShortControllerName(this string controllerName)
        {
            const string key = "Controller";
            if (!controllerName.EndsWith(key))
                throw new Exception("請使用Controller類別");

            var value = controllerName.Substring(0, controllerName.LastIndexOf(key));
            return value;
        }

        public static bool HasEnglish(this String Input)
        {
            foreach (var item in Input.ToCharArray())
            {
                if (char.IsLower(item) || char.IsUpper(item))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsNumber(this String aNumber)
        {
            int n;
            bool isNumeric = int.TryParse(aNumber, out n);
            return isNumeric;
        }

        /// <summary>
        /// 最少0.0 最多0.0.0.0 , 單一數字不能超過int
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static Version ToVersion(this String Input)
        {
            return new Version(Input); ;
        }
    }
}
