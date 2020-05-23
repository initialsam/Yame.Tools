using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.Helper
{
    /// <summary>
    /// .net 內建的 Version 有0.0到0.0.0.0的限制 單一數字只能是int
    /// 所以建立 可以比較更多的版本EX 0.0.0.0.0.0 單一數字可以到long
    /// </summary>
    public class CompareVersionHelper
    {
        /// <summary>
        /// 比較版本 version1 大於 version2 是 1 ,小於是-1,等於是0
        /// </summary>
        /// <param name="version1"></param>
        /// <param name="version2"></param>
        /// <returns></returns>
        public static int? CompareVersion(string version1, string version2)
        {
            if (String.IsNullOrWhiteSpace(version1))
            {
                return -1;
            }


            string[] parts1 = version1.Split('.');
            string[] parts2 = version2.Split('.');
            for (int i = 0, j = 0; i < parts1.Length || j < parts2.Length; i++, j++)
            {
                if (i == parts1.Length)
                {
                    while (j < parts2.Length)
                    {
                        if (isAllZeros(parts2[j++])) continue;
                        return -1;
                    }
                    break;
                }
                if (j == parts2.Length)
                {
                    while (i < parts1.Length)
                    {
                        if (isAllZeros(parts1[i++])) continue;
                        return 1;
                    }
                    break;
                }

                bool isParts1ConvertToInt64 = Int64.TryParse(parts1[i], out long p1);
                bool isParts2ConvertToInt64 = Int64.TryParse(parts2[i], out long p2);

                //轉型別成功
                if (isParts1ConvertToInt64 && isParts2ConvertToInt64)
                {
                    if (p1 < p2) return -1;
                    else if (p1 > p2) return 1;
                }
                else //轉型別失敗，則回傳 null
                {
                    return null;
                }
            }
            return 0;
        }

        private static bool isAllZeros(string s)
        {
            foreach (var c in s)
            {
                if (c != '0') return false;
            }
            return true;
        }

        /// <summary>
        /// 是否是 N.N.N.N
        /// N : 數字
        /// </summary>
        /// <returns></returns>
        public static bool IsNumberStrSplitWithPoint(string testStr)
        {
            if (string.IsNullOrWhiteSpace(testStr)) return false;

            string[] strArr = testStr.Split('.');
            foreach (var str in strArr)
            {
                if (!Int64.TryParse(str, out long kk1)) return false;

            }
            return true;
        }
    }
}
