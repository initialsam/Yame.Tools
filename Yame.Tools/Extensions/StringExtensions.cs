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
    }
}
