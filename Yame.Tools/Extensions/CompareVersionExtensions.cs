using System;
using System.Collections.Generic;
using System.Text;
using Yame.Tools.Helper;

namespace Yame.Tools.Extensions
{
    public static class CompareVersionExtensions
    {
        /// <summary>
        /// 大於
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool VersionGreaterThan(this string Input, string target)
        {
            return CompareVersionHelper.CompareVersion(Input,target) == 1;
        }

        /// <summary>
        /// 小於
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool VersionLessThan(this string Input, string target)
        {
            return CompareVersionHelper.CompareVersion(Input, target) == -1;
        }

        /// <summary>
        /// 大於等於
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool VersionGreaterThanOrEqual(this string Input, string target)
        {
            return CompareVersionHelper.CompareVersion(Input, target) >= 0;
        }

        /// <summary>
        /// 小於等於
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool VersionLessThanOrEqual(this string Input, string target)
        {
            return CompareVersionHelper.CompareVersion(Input, target) <= 0;
        }

        /// <summary>
        /// 等於
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool VersionEqual(this string Input, string target)
        {
            return Input == target;
        }

        /// <summary>
        /// 不等於
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool VersionNotEqual(this string Input, string target)
        {
            return Input != target;
        }
    }
}
