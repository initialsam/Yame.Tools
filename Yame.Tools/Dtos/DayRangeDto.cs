using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.NetCore.Dtos
{
    public class DayRangeDto
    {
        /// <summary>
        /// 當天的台北開始時間(UTC+8)
        /// </summary>
        public DateTime TaipeiStartDate { get; set; }

        /// <summary>
        /// 當天的台北結束時間(UTC+8)
        /// </summary>
        public DateTime TaipeiEndDate { get; set; }

        /// <summary>
        /// 當天的UTC開始時間
        /// </summary>
        public DateTime UTCStartDate { get; set; }

        /// <summary>
        /// 當天的UTC台北結束
        /// </summary>
        public DateTime UTCEndDate { get; set; }
    }
}
