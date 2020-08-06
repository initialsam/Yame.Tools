using System;
using System.Collections.Generic;
using System.Text;
using Yame.Tools.Helper;
using Yame.Tools.NetCore.Structs;

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

        /// <summary>
        /// 轉成DateTimeWithZone 時區是台北標準時間
        /// <para>value 不管是怎樣的時區 都會當作台北時間 然後去產生UTC時間</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTimeWithZone ToDateTimeWithZoneForTaipeiStandardTime(this DateTime value)
        {
            return new DateTimeWithZone(value, GetTaipeiStandardTimeTimeZoneInfo());
        }
        public static DateTime UtcToTaipeiStandardTime(this DateTime value)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(value, GetTaipeiStandardTimeTimeZoneInfo());
        }

        private static TimeZoneInfo GetTaipeiStandardTimeTimeZoneInfo()
        {
            return TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
        }
    }
}
