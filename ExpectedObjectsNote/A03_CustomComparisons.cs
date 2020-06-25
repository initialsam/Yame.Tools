using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpectedObjects;
using System.Collections.Generic;

namespace ExpectedObjectsNote
{
    [TestClass]
    public class A03_CustomComparisons
    {
        internal class Receipt
        {
            public string Name { get; set; }
            public DateTime DateTime { get; set; }
            public Customer Customer { get; set; }
            public Customer Customer2 { get; set; }
        }

        internal class Customer
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [TestMethod]
        public void CustomComparisons_ShouldMatch()
        {
            var expected = new 
            {
                Name = Expect.NotNull(),
                DateTime = Expect.Default<DateTime>(),
                Customer = Expect.Any<Customer>(),
                Customer2 = Expect.Any<Customer>(x=>x.FirstName == "foo")
            }.ToExpectedObject();


            var actual = new Receipt
            {
                Name = "John Doe",
                DateTime = DateTime.MinValue,
                Customer = new Customer(),
                Customer2 = new Customer() { FirstName = "foo" }
            };


            // observation
            expected.ShouldMatch(actual);
        }
    }
}



