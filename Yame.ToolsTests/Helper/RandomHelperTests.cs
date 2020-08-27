using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Yame.Tools.Helper.Tests
{
    [TestClass()]
    public class RandomHelperTests
    {
        [TestMethod()]
        public void GetRandomNumberTest()
        {
            //Arrange

            //Act
            var actual1 = RandomHelper.GetRandomNumber(6);
            var actual2 = RandomHelper.GetRandomNumber(6);
            //Assert
            actual1.Length.Should().Be(6);

            actual1.Should().NotBe(actual2);
        }
    }
}