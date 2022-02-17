using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Yame.Tools.Helper;

namespace Yame.Tools.NetCore.Helper
{
    public static class SaleDateHelper
    {
        public static bool IsSale(DateTime startDate, DateTime endDate, List<IBookingDateTime> bookingList)
        {
            return bookingList.Where(x =>
                (
                    //查詢結束時間落在營業區間內
                    x.StartTime <= endDate &&
                    x.EndTime >= endDate
                )
                ||
                (
                    //查詢時間包含整個銷售日期
                    x.StartTime > startDate &&
                    x.EndTime < endDate
                )
                ||
                (
                    //查詢開始時間落在營業區間內
                    x.StartTime <= startDate &&
                    x.EndTime >= startDate
                )
            ).Any();
        }
    }
}
