using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Yame.Tools.NetCore.Helper
{
    /// <summary>
    /// 存資料的時候 大量時可能會依照 分 天 週 月 來存表 所以要反查出來 日期區間是落在那些表
    /// </summary>
    public static class SeparateHelper
    {
        public static List<string> GetSeparateCollectionNameByTimeRange(DateTime startTime, DateTime endTime)
        {
            var list = new List<string>();
            //用月分表
            for (DateTime dt = startTime;
                         (new DateTime(dt.Year, dt.Month, 1)) <= (new DateTime(endTime.Year, endTime.Month, 1));
                         dt = dt.AddMonths(1))
            {
                list.Add(dt.ToString("yyyyMM"));
            }
            //用週分表
            var list2 = new List<string>();
            Dictionary<string, int> weekOfYearStrDic = new Dictionary<string, int>();
            for (DateTime dt = startTime; endTime.CompareTo(dt) > 0; dt = dt.AddDays(1.0))
            {
                string weekOfYearStr = $"{dt.Year}{new TaiwanCalendar().GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Sunday)}";
                if (!weekOfYearStrDic.ContainsKey(weekOfYearStr))
                {
                    weekOfYearStrDic.Add(weekOfYearStr, 0);
                }
            }
            list2.AddRange(weekOfYearStrDic.Keys.ToList());
            //用日分表
            var list3 = new List<string>();
            for (DateTime dt = startTime; endTime.CompareTo(dt) > 0; dt = dt.AddDays(1.0))
            {
                list3.Add(dt.ToString("yyyyMMdd"));
            }
            return list;
        }
    }
}
