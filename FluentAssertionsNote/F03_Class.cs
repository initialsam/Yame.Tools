using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FluentAssertionsNote
{
    [TestClass]
    public class F03_Class
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Company Company { get; set; }
        }
        public class Company
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [TestMethod]
        public void FluentAssertionsClass1()
        {
            var expected = new Product
            {
                Id = 1,
                Name = "Ball",
                Company = new Company { Name = "Google", Id =881 }
            };
            var actual = new Product
            {
                Id = 1,
                Name = "Ball",
                Company = new Company { Name = "Google", Id = 881 }
            };
            //物件內容相同
            actual.Should().BeEquivalentTo(expected);

            //var actual = new Product
            //{
            //    Id = 2,
            //    Name = "Ball",
            //    Company = new Company { Name = "Google", Id = 882 }
            //};
            //錯誤訊息是
            //Expected member Id to be 1, but found 2.
            //Expected member Company.Id to be 881, but found 882.
        }

        [TestMethod]
        public void FluentAssertionsClass2()
        {
            var expected = new Product
            {
                Id = 1,
                Name = "Ball",
                Company = new Company { Name = "Google", Id = 881 }
            };
            var actual = expected;
            //記憶體相同
            actual.Should().Be(expected);
        }
    }
}
