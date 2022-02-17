using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.Helper;
using Yame.ToolsTests.Dto;

namespace Yame.ToolsTests.Helper
{
    [TestClass()]
    public class BookingHelperTests
    {
        private static List<IBookingDateTime> GetBookingList(int startHour, int endHour)
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
        public void 只知道開始時間_預約開始時間重疊現有預約的開始時間算有衝突()
        {
            /*----------------------------------------------------
            現有     |-------------|
            新的     |--?
            ------------------------------------------------------*/
            var bookingList = GetBookingList(7, 8);
            var expected = true;

            var actual = BookingHelper.IsConflict(
                new DateTime(2019, 10, 1, 7, 0, 0), 
                bookingList);
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 只知道開始時間_預約開始時間重疊現有預約的結束時間不算有衝突()
        {
            /*----------------------------------------------------
            現有     |-------------|
            新的                   |--?
           ------------------------------------------------------*/
            var bookingList = GetBookingList(7, 8);
            var expected = false;

            var actual = BookingHelper.IsConflict(
                new DateTime(2019, 10, 1, 8, 0, 0), 
                bookingList);
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 只知道開始時間_預約開始時間落在現有預約之間算有衝突()
        {
            /*----------------------------------------------------
            現有     |-------------|
            新的            |--?       
          ------------------------------------------------------*/
            var bookingList = GetBookingList(7, 8);
            var expected = true;

            var actual = BookingHelper.IsConflict(
                new DateTime(2019, 10, 1, 7, 30, 0), 
                bookingList);
            actual.Should().Be(expected);
        }
        //---------------------
        [TestMethod()]
        public void 預約開始時間重疊現有預約的開始時間算有衝突()
        {
            /*----------------------------------------------------
            現有     |-------------|
            新的     |------|    
            ------------------------------------------------------*/
            var bookingList = GetBookingList(7, 8);
            var expected = true;

            var actual = BookingHelper.IsConflict(
                new DateTime(2019, 10, 1, 7, 0, 0), 
                new DateTime(2019, 10, 1, 7, 30, 0), 
                bookingList);
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 預約開始時間重疊現有預約的結束時間不算有衝突()
        {
            /*----------------------------------------------------
            現有     |-------------|
            新的                   |---------|    
            ------------------------------------------------------*/
            var bookingList = GetBookingList(7, 8);
            var expected = false;

            var actual = BookingHelper.IsConflict(
                new DateTime(2019, 10, 1, 8, 0, 0), 
                new DateTime(2019, 10, 1, 9, 0, 0), 
                bookingList);
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 預約結束時間重疊現有預約的開始時間不算有衝突()
        {
            /*----------------------------------------------------
            現有             |-------------|
            新的   |---------|    
            ------------------------------------------------------*/
            var bookingList = GetBookingList(7, 8);
            var expected = false;

            var actual = BookingHelper.IsConflict(
                new DateTime(2019, 10, 1, 6, 0, 0), 
                new DateTime(2019, 10, 1, 7, 0, 0), 
                bookingList);
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 預約結束時間重疊現有預約的結束時間算有衝突()
        {
            /*----------------------------------------------------
            現有             |-------------|
            新的         |-----------------|    
           ------------------------------------------------------*/
            var bookingList = GetBookingList(7, 8);
            var expected = true;

            var actual = BookingHelper.IsConflict(
                new DateTime(2019, 10, 1, 6, 50, 0), 
                new DateTime(2019, 10, 1, 8, 0, 0), 
                bookingList);
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 預約開始時間落在現有預約之間算有衝突()
        {
            /*----------------------------------------------------
            現有  |-------------|
            新的         |--------------|    
            ------------------------------------------------------*/
            var bookingList = GetBookingList(7, 8);
            var expected = true;

            var actual = BookingHelper.IsConflict(
                new DateTime(2019, 10, 1, 7, 30, 0),
                new DateTime(2019, 10, 1, 8, 30, 0), 
                bookingList);
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 預約結束時間落在現有預約之間算有衝突()
        {
            /*----------------------------------------------------
           現有              |-------------|
           新的     |--------------|    
           ------------------------------------------------------*/
            var bookingList = GetBookingList(7, 8);
            var expected = true;

            var actual = BookingHelper.IsConflict(
                new DateTime(2019, 10, 1, 6, 30, 0), 
                new DateTime(2019, 10, 1, 7, 30, 0), 
                bookingList);
            actual.Should().Be(expected);
        }
        [TestMethod()]
        public void 預約時間包含現有預約算有衝突()
        {
            /*----------------------------------------------------
           現有          |-------------|
           新的     |------------------------|    
           ------------------------------------------------------*/
            var bookingList = GetBookingList(0, 1);
            var expected = true;

            var actual = BookingHelper.IsConflict(
                new DateTime(2019, 09, 30, 23, 50, 0), 
                new DateTime(2019, 10, 1, 8, 10, 0), 
                bookingList);
            actual.Should().Be(expected);
        }
    }
}
