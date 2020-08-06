using System;
using System.Collections.Generic;
using System.Text;
using Yame.Tools.Extensions;
using Yame.Tools.NetCore.Dtos;

namespace Yame.Tools.Helper
{
    public static class DateRangeHelper
    {
        /// <summary>
        /// targetDate 為基準取得上周四+7天的 周範圍
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static WeekRangeDto GetWeekRangeFromLastThursday(DateTime date)
        {
            var targetDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc).ToDateTimeWithZoneForTaipeiStandardTime();
            DayOfWeek desiredDay = DayOfWeek.Thursday;
            int offsetAmount = (int)desiredDay - (int)(targetDate.LocalTime.DayOfWeek);
            var lastWeekThursday = targetDate.LocalTime.AddDays(-7 + offsetAmount).ToDateTimeWithZoneForTaipeiStandardTime();
            var endDay = lastWeekThursday.LocalTime.AddDays(7).ToDateTimeWithZoneForTaipeiStandardTime();

            var beginDate = GetFirstThursdayOfThisYear(lastWeekThursday.LocalTime).ToDateTimeWithZoneForTaipeiStandardTime();
            var weekCountName = $"{beginDate.LocalTime.Year}W{((endDay.LocalTime - beginDate.LocalTime).Days / 7)}";
            return new WeekRangeDto
            {
                WeekCountName = weekCountName,
                TaipeiStartDate = lastWeekThursday.LocalTime,
                TaipeiEndDate = endDay.LocalTime,
                UTCStartDate = lastWeekThursday.UniversalTime,
                UTCEndDate = endDay.UniversalTime
            };
        }

        /// <summary>
        /// targetDate 為基準取得前一天的範圍
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DayRangeDto GetYesterday(DateTime date)
        {
            var targetDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc).ToDateTimeWithZoneForTaipeiStandardTime();
            var startDay = targetDate.LocalTime.AddDays(-1).ToDateTimeWithZoneForTaipeiStandardTime();
            var endDay = targetDate;

            return new DayRangeDto
            {
                TaipeiStartDate = startDay.LocalTime,
                TaipeiEndDate = endDay.LocalTime,
                UTCStartDate = startDay.UniversalTime,
                UTCEndDate = endDay.UniversalTime
            };
        }

        /// <summary>
        /// 取今年第一個星期四
        /// </summary>
        /// <param name="givenDate"></param>
        /// <returns></returns>
        public static DateTime GetFirstThursdayOfThisYear(DateTime givenDate)
        {
            DateTime firstDayThisYear = new DateTime(givenDate.Year, 1, 1, 0, 0, 0);
            DayOfWeek desiredDay = DayOfWeek.Thursday;
            int diff = (int)desiredDay - (int)firstDayThisYear.DayOfWeek;
            if (diff < 0)
            {
                diff = diff + 7;
            }

            var firstThursdayOfThisYear = firstDayThisYear.AddDays(diff);

            return firstThursdayOfThisYear;
        }

        /// <summary>
        /// 取得區間內每個禮拜四
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<DateTime> GetDateRangeEachThursday(DateTime startDate, DateTime endDate)
        {
            var result = new List<DateTime>();
            for (DateTime dt = startDate; dt <= endDate; dt = dt.AddDays(7))
            {
                result.Add(GetNextWeekDay(dt, DayOfWeek.Thursday));
            }
            return result;
        }
        /// <summary>
        /// 取得區間內每一天
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<DateTime> GetDateRangeEachDay(DateTime startDate, DateTime endDate)
        {
            var result = new List<DateTime>();
            for (DateTime dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                result.Add(dt);
            }
            return result;
        }

        /// <summary>
        /// 取得下一個指定的週幾
        /// </summary>
        /// <param name="date"></param>
        /// <param name="desiredDay"></param>
        /// <returns></returns>
        public static DateTime GetNextWeekDay(DateTime date, DayOfWeek desiredDay)
        {
            var targetDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            int offsetAmount = (int)desiredDay - (int)targetDate.DayOfWeek;
            DateTime nextWeekDay = targetDate.AddDays(offsetAmount);
            return nextWeekDay;
        }
    }
}
