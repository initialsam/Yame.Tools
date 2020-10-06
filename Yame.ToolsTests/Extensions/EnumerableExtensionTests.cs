using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.NetCore.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Yame.ToolsTests.Dto;
using FluentAssertions;

namespace Yame.Tools.NetCore.Extensions.Tests
{
    [TestClass()]
    public class EnumerableExtensionTests
    {

        [TestMethod()]
        public void DistinctTest()
        {
            //Arrange
            var list = new List<DemoProduct>() 
            {
                new DemoProduct{ ProductId = 1, Name="t1" },
                new DemoProduct{ ProductId = 2, Name="t2" },
                new DemoProduct{ ProductId = 1, Name="t11" },
                new DemoProduct{ ProductId = 2, Name="t22" }
            };
            var expected = new List<DemoProduct>()
            {
                new DemoProduct{ ProductId = 1, Name="t1" },
                new DemoProduct{ ProductId = 2, Name="t2" }
            };

            //Act
            var actual = list.Distinct(x => x.ProductId);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}