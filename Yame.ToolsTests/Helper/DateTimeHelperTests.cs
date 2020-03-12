using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yame.Tools.Helper;

namespace Yame.ToolsTests.Helper
{
    [TestClass()]
    public class DateTimeHelperTests
    {
        [TestMethod]
        public void 取得用早上七點當分隔的一天的最後一秒()
        {
            //Arrange
            var date = new DateTime(2019, 10, 1);
            var expected = new DateTime(2019, 10, 2, 6, 59, 59);
            //Act
            var actual = DateTimeHelper.SetDayofLastSecByBySevenAm(date);
            //Assert
            actual.Should().Be(expected);
        }
        [TestMethod]
        public void 取得用早上七點當分隔的一天的第一秒()
        {
            //Arrange
            var date = new DateTime(2019, 10, 1);
            var expected = new DateTime(2019, 10, 1, 7, 00, 00);
            //Act
            var actual = DateTimeHelper.SetDayofFirstSecBySevenAm(date);
            //Assert
            actual.Should().Be(expected);
        }
        [TestMethod]
        public void 取得一天的最後一秒()
        {
            //Arrange
            var date = new DateTime(2019, 10, 1);
            var expected = new DateTime(2019, 10, 1, 23, 59, 59);
            //Act
            var actual = DateTimeHelper.SetDayofLastSec(date);
            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void 兩個日期10號跟1號之間的天數是9天()
        {
            //Arrange
            var end = new DateTime(2019, 10, 10);
            var start = new DateTime(2019, 10, 1);
            int expected = 9;
            //Act
            var actual = DateTimeHelper.GetDays(end, start);
            //Assert
            actual.Should().Be(expected);
        }
        [TestMethod]
        public void 兩個日期小於1天也要回傳1天()
        {
            //Arrange
            var end = new DateTime(2019, 10, 1, 20, 21, 59);
            var start = new DateTime(2019, 10, 1, 20, 21, 00);

            int expected = 1;
            //Act
            var actual = DateTimeHelper.GetDays(end, start);
            //Assert
            actual.Should().Be(expected);
        }
        [TestMethod]
        public void 兩個日期小於2天也要回傳2天()
        {
            //Arrange
            var end = new DateTime(2019, 10, 2, 20, 21, 59);
            var start = new DateTime(2019, 10, 1, 20, 21, 00);

            int expected = 2;
            //Act
            var actual = DateTimeHelper.GetDays(end, start);
            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod()]
        public void 取得2019年12月31號的第一天應該是2019年12月1號()
        {
            //Arrange
            var date = new DateTime(2019, 12, 31);

            var expected = new DateTime(2019, 12, 1);
            //Act
            var actual = DateTimeHelper.GetMonthFirstDay(date);
            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod()]
        public void 取得2019年2月5號的最後一天應該是2019年2月28號()
        {
            //Arrange
            var date = new DateTime(2019, 2, 5);

            var expected = new DateTime(2019, 2, 28);
            //Act
            var actual = DateTimeHelper.GetMonthLastDay(date);
            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod()]
        public void 取得2020年2月5號的最後一天應該是2020年2月29號()
        {
            //Arrange
            var date = new DateTime(2020, 2, 15);

            var expected = new DateTime(2020, 2, 29);
            //Act
            var actual = DateTimeHelper.GetMonthLastDay(date);
            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod()]
        public void 取得年紀_生日還沒過應該是19()
        {
            //Arrange
            var today = new DateTime(2020, 2, 13);
            var birthday = new DateTime(2000, 2, 14);
            var expected = 19;
            //Act
            var actual = DateTimeHelper.GetAge(today, birthday);
            //Assert
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 取得年紀_生日以過應該是20()
        {
            //Arrange
            var today = new DateTime(2020, 2, 14);
            var birthday = new DateTime(2000, 2, 14);
            var expected = 20;
            //Act
            var actual = DateTimeHelper.GetAge(today, birthday);
            //Assert
            actual.Should().Be(expected);
        }
    }
}
