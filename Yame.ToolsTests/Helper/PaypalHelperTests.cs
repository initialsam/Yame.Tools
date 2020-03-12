using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Yame.Tools.Helper.Tests
{
    [TestClass()]
    public class PaypalHelperTests
    {
        [DataTestMethod]
        [DataRow("TWD", "500.00", "TWD : 500")]
        [DataRow("USD", "9.99", "USD : 9.99")]
        public void GetPriceStringTest(string currency, string value, string expected)
        {
            //Arrange

            //Act
            var actual = PaypalHelper.GetPriceString(currency, value);
            //Assert
            actual.Should().Be(expected);
        }
    }
}