using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpLab.A01_Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CSharpLab.A01_Generics.Tests
{
    [TestClass()]
    public class A01_Generics_DemoTests
    {
        [TestMethod()]
        [TestCategory("Generics")]
        public void 傳入空字串_應回傳null()
        {
            //arrange 
            //act
            var actual = A01_Generics_Demo.Demo("");

            //assert
            actual.Should().BeNull();
        }

        [TestMethod()]
        [TestCategory("Generics")]
        public void 傳入正確json格式_應回傳APIData()
        {
            //arrange 
            var json = "{\"Version\":\"V1\",\"Data\":{\"ID\":87,\"Name\":\"產品\"},\"ErrorCode\":0}";

            var expected = new APIData<Product, int>()
            {
                Version = "V1",
                ErrorCode = 0,
                Data = new Product()
                {
                    ID = 87,
                    Name = "產品"
                }
            };

            //act
            var actual = A01_Generics_Demo.Demo(json);

            //assert
            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod()]
        [TestCategory("Generics")]
        public void 傳入錯誤字串__應回傳APIData空物件()
        {
            //arrange 
            var json = "aaaa";

            var expected = new APIData<Product, int>();

            //act
            var actual = A01_Generics_Demo.Demo(json);

            //assert
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
