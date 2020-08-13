using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Yame.Tools.NetCore.CustomAttribute;
using System.Linq;

namespace Yame.Tools.CustomAttribute.Tests
{
    [TestClass()]
    public class OrderAttributeTests
    {
        [TestMethod()]
        public void ProductDto反射可以依照Order排序()
        {
            //Arrange
            var productDto = new ProductDto { ProductId=1, Name = "ABC",Owner="Sam" };
            var expected = new List<string> { "ProductId", "Owner", "Name" };
            //Act
            var properties = (from property in typeof(ProductDto).GetProperties()
                              where Attribute.IsDefined(property, typeof(OrderAttribute))
                              orderby ((OrderAttribute)property
                                        .GetCustomAttributes(typeof(OrderAttribute), false)
                                        .Single()).Order
                              select property).ToList();
            var actual = properties.Select(x => x.Name).ToList();
            //Assert
            actual.Should().Equal(expected);
        }

        
    }

    public class ProductDto
    {
        [Order(1)]
        public int ProductId { get; set; }
        [Order(3)]
        public string Name { get; set; }
        [Order(2)]
        public string Owner { get; set; }
    }

}