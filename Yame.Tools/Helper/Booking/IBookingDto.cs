using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.Helper
{
    public interface IBookingDateTime
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
