using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Yame.Tools.Extensions.Tests
{
    [TestClass()]
    public class VersionExtensionsTests
    {
        [TestMethod()]
        public void GreaterThanTest()
        {
            var actual = "2.30.0".ToVersion().GreaterThan("2.29.0".ToVersion());
            actual.Should().BeTrue();

            actual = "1.99.99".ToVersion().GreaterThan("2.12.34".ToVersion());
            actual.Should().BeFalse();

            actual = "2.1.1".ToVersion().GreaterThan("2.1.1".ToVersion());
            actual.Should().BeFalse();
        }

        [TestMethod()]
        public void LessThanTest()
        {
            var actual = "2.30.0".ToVersion().LessThan("2.30.1".ToVersion());
            actual.Should().BeTrue();

            actual = "2.0.0".ToVersion().LessThan("1.99.98".ToVersion());
            actual.Should().BeFalse();

            actual = "2.1.1".ToVersion().LessThan("2.1.1".ToVersion());
            actual.Should().BeFalse();
        }

        [TestMethod()]
        public void GreaterThanOrEqualTest()
        {
            var actual = "2.30.0".ToVersion().GreaterThanOrEqual("2.29.99".ToVersion());
            actual.Should().BeTrue();

            actual = "2.30.0".ToVersion().GreaterThanOrEqual("3.0.0".ToVersion());
            actual.Should().BeFalse();

            actual = "2.30.0".ToVersion().GreaterThanOrEqual("2.30.0".ToVersion());
            actual.Should().BeTrue();
        }

        [TestMethod()]
        public void LessThanOrEqualTest()
        {
            var actual = "2.29.0".ToVersion().LessThanOrEqual("2.30.1".ToVersion());
            actual.Should().BeTrue();

            actual = "2.99.0".ToVersion().LessThanOrEqual("2.88.0".ToVersion());
            actual.Should().BeFalse();

            actual = "2.30.0".ToVersion().LessThanOrEqual("2.30.0".ToVersion());
            actual.Should().BeTrue();
        }

        [TestMethod()]
        public void EqualTest()
        {
            var actual = "2.30.0".ToVersion().Equal("2.30.0".ToVersion());
            actual.Should().BeTrue();

            actual = "2.30.0".ToVersion().Equal("2.30.00".ToVersion());
            actual.Should().BeTrue();

            actual = "2.30.0".ToVersion().Equal("2.30.1".ToVersion());
            actual.Should().BeFalse();
        }

        [TestMethod()]
        public void NotEqualTest()
        {
            var actual = "1.1.0".ToVersion().NotEqual("1.1.1".ToVersion());
            actual.Should().BeTrue();

            actual = "2.30.0".ToVersion().NotEqual("2.30.00".ToVersion());
            actual.Should().BeFalse();

        }
    }
}