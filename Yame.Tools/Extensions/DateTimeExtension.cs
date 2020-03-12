using System;
using System.Collections.Generic;
using System.Text;
using Yame.Tools.Helper;

namespace Yame.Tools.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// DateTime 轉顯示字串 yyyy/MM/dd HH:mm
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToDisplayDateTime(this DateTime source)
        {
            return source.ToString(FormatHelper.DateTimeFormat);
        }

        /// <summary>
        /// DateTime 轉顯示字串 yyyy/MM/dd
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToDisplayDate(this DateTime source)
        {
            return source.ToString(FormatHelper.DateFormat);
        }

        /// <summary>
        /// DateTime 轉顯示字串 HH:mm
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToDisplayTime(this DateTime source)
        {
            return source.ToString(FormatHelper.TimeFormat);
        }

        /// <summary>
        /// DateTime 轉顯示字串 yyyy_MM_dd_HH_mm_ss
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToDisplayFileName(this DateTime source)
        {
            return source.ToString(FormatHelper.DateTimeFileName);
        }

        /// <summary>
        /// DateTime 轉顯示字串 yyyyMMdd
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToDisplayDateWithoutSlash(this DateTime source)
        {
            return source.ToString(FormatHelper.DateFormatWithoutSlash);
        }

        /// <summary>
        /// Smarterasp.Net DB備份日期格式 12_31_2019
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToDisplayDateFormatForSmarteraspDb(this DateTime source)
        {
            return source.ToString(FormatHelper.DateFormatForSmarteraspDb);
        }


    }
}
