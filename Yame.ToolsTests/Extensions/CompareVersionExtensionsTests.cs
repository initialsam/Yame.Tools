using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Yame.Tools.Extensions.Tests
{
    [TestClass()]
    public class CompareVersionExtensionsTests
    {
        [TestMethod()]
        public void GreaterThanTest()
        {
            var actual = "2.30.0".VersionGreaterThan("2.29.0");
            actual.Should().BeTrue();

            actual = "1.99.99".VersionGreaterThan("2.12.34");
            actual.Should().BeFalse();

            actual = "2.1.1".VersionGreaterThan("2.1.1");
            actual.Should().BeFalse();
        }

        [TestMethod()]
        public void LessThanTest()
        {
            var actual = "2.30.0".VersionLessThan("2.30.1");
            actual.Should().BeTrue();

            actual = "2.0.0".VersionLessThan("1.99.98");
            actual.Should().BeFalse();

            actual = "2.1.1".VersionLessThan("2.1.1");
            actual.Should().BeFalse();
        }

        [TestMethod()]
        public void GreaterThanOrEqualTest()
        {
            var actual = "2.30.0".VersionGreaterThanOrEqual("2.29.99");
            actual.Should().BeTrue();

            actual = "2.30.0".VersionGreaterThanOrEqual("3.0.0");
            actual.Should().BeFalse();

            actual = "2.30.0".VersionGreaterThanOrEqual("2.30.0");
            actual.Should().BeTrue();
        }

        [TestMethod()]
        public void LessThanOrEqualTest()
        {
            var actual = "2.29.0".VersionLessThanOrEqual("2.30.1");
            actual.Should().BeTrue();

            actual = "2.99.0".VersionLessThanOrEqual("2.88.0");
            actual.Should().BeFalse();

            actual = "2.30.0".VersionLessThanOrEqual("2.30.0");
            actual.Should().BeTrue();
        }

        [TestMethod()]
        public void EqualTest()
        {
            var actual = "2.30.0".VersionEqual("2.30.0");
            actual.Should().BeTrue();

            actual = "2.30.0".VersionEqual("2.30.00");
            actual.Should().BeTrue();

            actual = "2.30.0".VersionEqual("2.30.1");
            actual.Should().BeFalse();
        }

        [TestMethod()]
        public void NotEqualTest()
        {
            var actual = "1.1.0".VersionNotEqual("1.1.1");
            actual.Should().BeTrue();

            actual = "2.30.0".VersionNotEqual("2.30.00");
            actual.Should().BeFalse();

        }
    }
}