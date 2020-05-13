using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Yame.Tools.CustomAttribute.Tests
{
    [TestClass()]
    public class StringHelperTests
    {
        [TestMethod()]
        public void AutoTrim_字串過長_截斷屬性值()
        {
            //Arrange
            var fakeClass = new FakeClass { Name = "AAAAAAA" };
            var actual = "AAA"; 
            //Act
            StringHelper.AutoTrim(fakeClass);
            //Assert
            actual.Should().Be(fakeClass.Name);
        }

        [TestMethod()]
        public void AutoTrim_字串太短或null_不處理()
        {
            //Arrange
            var fakeClass = new FakeClass { Name = "AA" };
            var actual = "AA";
            //Act
            StringHelper.AutoTrim(fakeClass);
            //Assert
            actual.Should().Be(fakeClass.Name);

            //Arrange
            var fakeClass2 = new FakeClass();
            //Act
            StringHelper.AutoTrim(fakeClass2);
            //Assert
            fakeClass2.Name.Should().BeNull();
        }
    }

    public class FakeClass
    {
        [AutoTrim(3)]
        public string Name { get; set; }
    }

}