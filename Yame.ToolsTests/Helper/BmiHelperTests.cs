using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Yame.Tools.Helper.Tests
{
    [TestClass()]
    public class BmiHelperTests
    {
        [TestMethod()]
        public void GetBmiTest()
        {
            //Arrange
            int height = 180;
            int weight = 87;

            var expected = 26.9f;
            //Act
            var actual = BmiHelper.GetBmi(height, weight);
            //Assert
            actual.Should().Be(expected);
        }
    }
}