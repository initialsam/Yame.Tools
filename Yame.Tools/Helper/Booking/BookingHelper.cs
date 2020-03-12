using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yame.Tools.Helper
{
    public static class BookingHelper
    {

        /// <summary>
        /// 新的預約有沒有衝突
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="bookingList"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static bool IsConflict(DateTime startDate, DateTime endDate, List<IBookingDateTime> bookingList)
        {
            return bookingList.Where(x =>
                (
                    x.StartTime < endDate &&
                    x.EndTime >= endDate
                ) ||
                (
                    x.StartTime > startDate &&
                    x.EndTime < endDate
                ) ||
                (
                    x.StartTime <= startDate &&
                    x.EndTime > startDate
                )
            ).Any();
        }

        /// <summary>
        /// 新的預約開始時間有沒有衝突
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="bookingList"></param>
        /// <returns></returns>
        public static bool IsConflict(DateTime startDate, List<IBookingDateTime> bookingList)
        {
            return bookingList.Where(x => x.StartTime <= startDate &&
                                          x.EndTime > startDate
            ).Any();
        }
    }
}
