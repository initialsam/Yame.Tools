using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpectedObjects;
using System.Collections.Generic;

namespace ExpectedObjectsNote
{
    [TestClass]
    public class A02_Ignoring
    {
        internal class Address
        {
            public string AddressLineOne { get; set; }
            public string AddressLineTwo { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
        }

        internal class Customer
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Address Address { get; set; }
        }

        [TestMethod]
        public void ComparingEqualCustomers_ShouldBeEqual()
        {
            // establish context

            var expectedCustomer = new Customer
            {
                FirstName = "Silence",
                LastName = "Dogood",
                Address = new Address
                {
                    AddressLineOne = "The New-England Courant",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02114"
                }
            }.ToExpectedObject(ctx => ctx.Ignore(x => x.Address.AddressLineTwo));

            var actualCustomer = new Customer
            {
                FirstName = "Silence",
                LastName = "Dogood",
                Address = new Address
                {
                    AddressLineOne = "The New-England Courant",
                    AddressLineTwo = "3 King Street",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02114"
                }
            };


            // observation
            expectedCustomer.ShouldEqual(actualCustomer);
        
    }
    }

    
}
