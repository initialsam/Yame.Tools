using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Yame.Tools.Extensions.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [DataTestMethod]
        [DataRow("LoginController", "Login")]
        [DataRow("HomeController", "Home")]
        public void GetShortControllerNameTest(string fullControllerName, string shortControllerName)
        {
            //Arrange
       
            //Act
            var actual = fullControllerName.GetShortControllerName();
            //Assert
            actual.Should().Be(shortControllerName);
        }
    }
}