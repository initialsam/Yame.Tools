using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpectedObjects;

namespace ExpectedObjectsNote
{
    [TestClass]
    public class A00_HelloExpectedObjects
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
                    AddressLineTwo = "3 Queen Street",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02114"
                }
            }.ToExpectedObject();

            var actualCustomer = new Customer
            {
                FirstName = "Silence",
                LastName = "Dogood",
                Address = new Address
                {
                    AddressLineOne = "The New-England Courant",
                    AddressLineTwo = "3 Queen Street",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02114"
                }
            };


            // observation
            expectedCustomer.ShouldEqual(actualCustomer);
        }

        [TestMethod]
        public void ComparingEqualCustomers_ShouldMatch()
        {
            // establish context
            var expectedCustomer = new
            {
                FirstName = "Silence",
                Address = new
                {
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02114"
                }
            }.ToExpectedObject();

            var actualCustomer = new Customer
            {
                FirstName = "Silence",
                LastName = "Dogood",
                Address = new Address
                {
                    AddressLineOne = "The New-England Courant",
                    AddressLineTwo = "3 Queen Street",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02114"
                }
            };


            // observation
            expectedCustomer.ShouldMatch(actualCustomer);
        }

        [TestMethod]
        public void ComparingEqualCustomers_ShouldBeEqual_Failure_Output()
        {
            // establish context
            var expectedCustomer = new Customer
            {
                FirstName = "Silence",
                LastName = "Dogood",
                Address = new Address
                {
                    AddressLineOne = "The New-England Courant",
                    AddressLineTwo = "3 Queen Street",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02114"
                }
            }.ToExpectedObject();

            var actualCustomer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                Address = new Address
                {
                    AddressLineOne = "The New-England Courant",
                    AddressLineTwo = string.Empty,
                    City = "Boston",
                    State = string.Empty,
                    PostalCode = "02114"
                }
            };


            // observation
            expectedCustomer.ShouldEqual(actualCustomer);

            /* 錯誤訊息
1) Customer.FirstName:

  Expected:
    "Silence"
    
  Actual:
    "John"
    


2) Customer.LastName:

  Expected:
    "Dogood"
    
  Actual:
    "Doe"
    


3) Customer.Address.AddressLineTwo:

  Expected:
    "3 Queen Street"
    
  Actual:
    String.Empty
    


4) Customer.Address.State:

  Expected:
    "MA"
    
  Actual:
    String.Empty
*/
        }
    }

   
}
