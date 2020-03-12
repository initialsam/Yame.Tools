using System;
using Yame.Tools.Helper;

namespace Yame.ToolsTests.Dto
{
    internal class BookingDateTime : IBookingDateTime
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}