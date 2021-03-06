﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Yame.Tools.NetCore.Dtos;

namespace Yame.Tools.Helper
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// 取得兩個日期之間的天數
        /// </summary>
        /// <param name="end"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static int GetDays(DateTime end, DateTime start)
        {
            var totalDays = new TimeSpan(end.Ticks - start.Ticks).TotalDays;

            return (int)Math.Ceiling(totalDays);
        }
        /// <summary>
        /// 設定日期的時間為23:59:59 用於結束時間
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime SetDayofLastSec(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }
        /// <summary>
        /// 設定日期的時間為07:00:00 用於開始時間
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime SetDayofFirstSecBySevenAm(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 07, 00, 00);
        }

        /// <summary>
        /// 設定日期的時間為加一天的06:59:59 用於結束時間
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime SetDayofLastSecByBySevenAm(DateTime date)
        {
            var temp = date.AddDays(1);
            return new DateTime(temp.Year, temp.Month, temp.Day, 06, 59, 59);
        }

        /// <summary>
        /// 設定日期的時間為23:59:59 用於結束時間
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime? SetDayofLastSec(DateTime? date)
        {
            if (date.HasValue)
            {
                return SetDayofLastSec(date.Value);
            }

            return default(DateTime?);
        }

        /// <summary>
        /// 取得月份第一天日期
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DateTime GetMonthFirstDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 取得月份第一天日期
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DateTime GetMonthFirstDay(int year, int month)
        {
            try
            {
                return new DateTime(year, month, 1);
            }
            catch
            {
                var today = NowTaipeiTime();
                return new DateTime(today.Year, today.Month, 1);

            }

        }

        /// <summary>
        /// 取得月份最後一天日期
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DateTime GetMonthLastDay(DateTime date)
        {
            var firstDayOfMonth = GetMonthFirstDay(date);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            return lastDayOfMonth;
        }

        /// <summary>
        /// 取得月份最後一天日期
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DateTime GetMonthLastDay(int year, int month)
        {
            return GetMonthLastDay(GetMonthFirstDay(year, month));
        }

        /// <summary>
        /// 取得現在時間 (+8時區)
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DateTime NowTaipeiTime()
        {
            return DateTime.UtcNow.AddHours(8);
        }



        /// <summary>
        /// 取得現在時間 (+8時區)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime TaipeiTime(DateTime datetime)
        {
            return datetime.ToUniversalTime().AddHours(8);
        }


        /// <summary>
        /// 合併第一個參數的日期跟第二個參數的時間
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime MergeDateAndTime(DateTime date, DateTime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
        }

        /// <summary>
        /// 取得年紀
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int GetAge(DateTime today, DateTime birthday)
        {
            // Calculate the age.
            var age = today.Year - birthday.Year;
            // Go back to the year the person was born in case of a leap year
            if (birthday.Date > today.AddYears(-age)) age--;
            return age;

        }

        public static void SetSearchTime(DateTime? startDate, DateTime? endDate, out DateTime startTime, out DateTime endTime)
        {
            startTime = startDate.HasValue ?
                                startDate.Value :
                                DateTimeHelper.GetMonthFirstDay(DateTimeHelper.NowTaipeiTime());
            endTime = endDate.HasValue ?
                            endDate.Value :
                            DateTimeHelper.GetMonthLastDay(DateTimeHelper.NowTaipeiTime());
            startTime = DateTimeHelper.SetDayofFirstSecBySevenAm(startTime);
            endTime = DateTimeHelper.SetDayofLastSecByBySevenAm(endTime);
        }

        /// <summary>
        /// 無條件捨去取最近的前一個 15分鐘刻度的時間。(ex. 9:10 => 9:00、 9:16 => 9:15)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime TimeRoundDown(DateTime input)
        {
            return new DateTime(input.Year, input.Month, input.Day, input.Hour, input.Minute, 0).AddMinutes(-input.Minute % 15);
        }

        /// <summary>
        /// 無條件捨去取最近的前一個 5分鐘刻度的時間。(ex. 9:10 => 9:05、 9:16 => 9:15)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime TimeRoundDown5Min(DateTime input)
        {
            return new DateTime(input.Year, input.Month, input.Day, input.Hour, input.Minute, 0).AddMinutes(-input.Minute % 5);
        }


        public static DateTime NowTaipeiDatetime
        {
            get
            {
                return TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.Utc, TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time"));
            }
        }

        /// <summary>
        /// 文字時間 加減?天 再轉回文字時間
        /// </summary>
        /// <param name="date">文字時間</param>
        /// <param name="format">時間格式</param>
        /// <param name="day">計算天數</param>
        /// <returns></returns>
        public static string StringDate(string date, string format, int day)
        {
            var prdt = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
            var result = prdt.AddDays(day).ToString(format);
            return result;
        }

        /// <summary>
        /// 檢查是否有重疊時間 , 若有重疊回傳重疊活動
        /// </summary>
        /// <param name="events"></param>
        /// <param name="overlapNameErrorMessage"></param>
        /// <returns></returns>
        public static bool IsEventOverlap(IEnumerable<EventDto> events, out string overlapNameErrorMessage)
        {
            DateTime endPrior = DateTime.MinValue;
            string endNamedPrior = string.Empty;
            foreach (EventDto eventDto in events.OrderBy(x => x.StartTime))
            {
                if (endPrior >= eventDto.StartTime)
                {
                    overlapNameErrorMessage = $"{endNamedPrior} 和 {eventDto.EventName} 活動時間有衝突";
                    return true;
                }

                endPrior = eventDto.EndTime;
                endNamedPrior = eventDto.EventName;
            }
            overlapNameErrorMessage = string.Empty;
            return false;
        }
    }
}
