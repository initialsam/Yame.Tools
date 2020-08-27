using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yame.Tools.Helper
{
    public static class RandomHelper
    {
        /// <summary>
        /// 取得隨機數字
        /// </summary>
        /// <param name="length">隨機數字的長度</param>
        /// <returns></returns>
        public static string GetRandomNumber(int length)
        {
            var chars = "0123456789";
            var random = new Random();
            var result= new string(
               Enumerable.Repeat(chars, length)
                         .Select(s => s[random.Next(s.Length)])
                         .ToArray());
            return result;
        }
    }
}
