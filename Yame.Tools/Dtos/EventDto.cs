using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.NetCore.Dtos
{
    public class EventDto
    {
        /// <summary>
        /// 活動名稱
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// 開始時間
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
