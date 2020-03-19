using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Yame.FeatureTests.Dto
{
    class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsVip { get; set; }
        public int Age { get; set; }

        public static string DoSomethingFail()
        {
            throw new ArgumentException("DoSomethingFail");
        }

        public static string DoFailTwo()
        {
            throw new ArgumentException("DoFailTwo");
        }

    }
}
