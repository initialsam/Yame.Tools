using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.NetCore.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Yame.Tools.Helper;

namespace Yame.Tools.NetCore.Helper.Tests
{
    [TestClass()]
    public class SaleDateHelperTests
    {
        private static List<IBookingDateTime> GetSaleList(int startHour, int endHour)
        {
            return new List<IBookingDateTime>
{
                new BookingDateTime
                {
                    StartTime = new DateTime(2019,10,1,startHour,0,0),
                    EndTime = new DateTime(2019,10,1,endHour,0,0)
                }
            };
        }
        [TestMethod()]
        public void IsSaleTest()
        {
            Assert.Fail();
        }
    }
}