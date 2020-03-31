using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xunit;
using Xunit.Abstractions;

namespace Yame.XUnitTests
{
    public class MemberData
    {
        private readonly ITestOutputHelper _output;

        public MemberData(ITestOutputHelper testOutputHelper)
        {
            _output = testOutputHelper;
        }

        [Fact]
        public void Test_Fact()
        {
            //Arrange
            var a = 1;
            var b = 2;
            var expected = 3;
            //Act
            var actual = a + b;
            //Assert
            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> GetTestData
        {
            get
            {
                yield return new object[]{
                   1,
                   new DateTime(1,1,1,0,0,1),
                };
                yield return new object[]{
                   2,
                   new DateTime(1,1,1,0,0,2),
                };
            }
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Test_Theory(
            int sec,
            DateTime expected)
        {
            //Arrange
            //Act
            var actual = DateTime.MinValue.AddSeconds(sec);
            //Assert
            actual.Should().Be(expected);
        }
    }
}
