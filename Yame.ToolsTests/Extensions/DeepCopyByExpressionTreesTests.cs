using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.NetCore.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
namespace Yame.Tools.NetCore.Extensions.Tests
{
    class TestA
    {
        public int Id { get; set; }
        public TestB TestB { get; set; }
    }
    class TestB
    {
        public String Name { get; set; }
        public TestC TestC { get; set; }
    }
    class TestC
    {
        public List<int> List { get; set; }
    }
    [TestClass()]
    public class DeepCopyByExpressionTreesTests
    {
        [TestMethod()]
        public void DeepCopyTest()
        {
            //Arrange
            var model = new TestA()
            {
                Id = 1,
                TestB = new TestB()
                {
                    Name = "BB",
                    TestC = new TestC()
                    {
                        List = new List<int> { 4, 5, 6 }
                    }
                }
            };
            //Act
            var actual = model.DeepCopy();
            //Assert
            actual.Should().BeEquivalentTo(model);
        }
    }
}