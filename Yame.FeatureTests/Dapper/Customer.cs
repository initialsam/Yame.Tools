using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.FeatureTests.Dapper
{
    public class Customer
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
