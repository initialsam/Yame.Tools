using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Yame.Tools.Helper.Tests
{
    [TestClass()]
    public class HotfixHelperTests
    {
        [DataTestMethod]
        [DataRow("KB666777", "KB666777")]
        [DataRow("KB6667778", "KB6667778")]
        [DataRow("ABCDE", "")]
        public void GetHotfixTest(string content, string expected)
        {
            //Arrange

            //Act
            var actual = HotfixHelper.GetHotfix(content);
            //Assert
            actual.Should().Be(expected);
        }
    }
}