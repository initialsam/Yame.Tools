using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.NetCore.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Yame.Tools.Helper;
using Yame.ToolsTests.Dto;
using FluentAssertions;

namespace Yame.Tools.NetCore.Helper.Tests
{
    [TestClass()]
    public class SaleDateHelperTests
    {
        private static List<IBookingDateTime> GetSaleList(int startDay, int endDay)
        {
            return new List<IBookingDateTime>
            {
                new BookingDateTime
                {
                    StartTime = new DateTime(2022,3,startDay,0,0,0),
                    EndTime = new DateTime(2022,3,endDay,0,0,0)
                }
            };
        }

        [TestMethod()]
        public void 查詢條件在銷售區間內1()
        {
            var saleList = GetSaleList(10, 20);
            var expected = true;
            var actual = SaleDateHelper.IsSale(
              new DateTime(2022, 3, 10, 0, 0, 0),
              new DateTime(2022, 3, 20, 0, 0, 0),
              saleList);
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 查詢條件在銷售區間內2()
        {
            var saleList = GetSaleList(10, 20);
            var expected = true;
            var actual = SaleDateHelper.IsSale(
              new DateTime(2022, 3, 11, 0, 0, 0),
              new DateTime(2022, 3, 19, 0, 0, 0),
              saleList);
            actual.Should().Be(expected);
        }

        [TestMethod()]
        public void 查詢條件包含銷售開始日1()
        {
            var saleList = GetSaleList(10, 20);
            var expected = true;
            var actual = SaleDateHelper.IsSale(
              new DateTime(2022, 3, 5, 0, 0, 0),
              new DateTime(2022, 3, 15, 0, 0, 0),
              saleList);
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 查詢條件包含銷售開始日2()
        {
            var saleList = GetSaleList(10, 20);
            var expected = true;
            var actual = SaleDateHelper.IsSale(
              new DateTime(2022, 3, 5, 0, 0, 0),
              new DateTime(2022, 3, 10, 0, 0, 0),
              saleList);
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 查詢條件包含銷售開始日3()
        {
            var saleList = GetSaleList(10, 20);
            var expected = true;
            var actual = SaleDateHelper.IsSale(
              new DateTime(2022, 3, 10, 0, 0, 0),
              new DateTime(2022, 3, 15, 0, 0, 0),
              saleList);
            actual.Should().Be(expected);
        }

        [TestMethod()]
        public void 查詢條件包含銷售結束日1()
        {
            var saleList = GetSaleList(10, 20);
            var expected = true;
            var actual = SaleDateHelper.IsSale(
              new DateTime(2022, 3, 15, 0, 0, 0),
              new DateTime(2022, 3, 25, 0, 0, 0),
              saleList);
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 查詢條件包含銷售結束日2()
        {
            var saleList = GetSaleList(10, 20);
            var expected = true;
            var actual = SaleDateHelper.IsSale(
              new DateTime(2022, 3, 15, 0, 0, 0),
              new DateTime(2022, 3, 20, 0, 0, 0),
              saleList);
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 查詢條件包含銷售結束日3()
        {
            var saleList = GetSaleList(10, 20);
            var expected = true;
            var actual = SaleDateHelper.IsSale(
              new DateTime(2022, 3, 20, 0, 0, 0),
              new DateTime(2022, 3, 25, 0, 0, 0),
              saleList);
            actual.Should().Be(expected);
        }

        [TestMethod()]
        public void 查詢條件包含銷售區間()
        {
            var saleList = GetSaleList(10, 20);
            var expected = true;
            var actual = SaleDateHelper.IsSale(
              new DateTime(2022, 3, 5, 0, 0, 0),
              new DateTime(2022, 3, 25, 0, 0, 0),
              saleList);
            actual.Should().Be(expected);
        }

        [TestMethod()]
        public void 查詢條件在銷售區間之前不算銷售()
        {
            var saleList = GetSaleList(10, 20);
            var expected = false;
            var actual = SaleDateHelper.IsSale(
              new DateTime(2022, 3, 1, 0, 0, 0),
              new DateTime(2022, 3, 5, 0, 0, 0),
              saleList);
            actual.Should().Be(expected);
        }

        [TestMethod()]
        public void 查詢條件在銷售區間之後不算銷售()
        {
            var saleList = GetSaleList(10, 20);
            var expected = false;
            var actual = SaleDateHelper.IsSale(
              new DateTime(2022, 3, 25, 0, 0, 0),
              new DateTime(2022, 3, 30, 0, 0, 0),
              saleList);
            actual.Should().Be(expected);
        }
    }
}