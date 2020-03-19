using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.HttpStatusCode;
using Yame.FeatureTests.Dto;


namespace Yame.FeatureTests
{
    [TestClass]
    public class CSharp6
    {
        [TestMethod]
        public void Test_CSharp6_NullConditionalOperators()
        {
            var a = new Point(9,8);

            var emp1 = new Employee { FirstName = "A", LastName = "B", IsManager = false, YearWorked = 2 };

            //Original
            if (emp1 != null)
            {
                var lName = emp1.LastName;
            }

            //New Null-conditional operators
            var lName2 = emp1?.LastName;

            var lName3 = emp1?.LastName ?? "No LastName";
        }

        [TestMethod]
        public void Test_CSharp6_nameof()
        {
            var emp1 = new Employee { FirstName = "A", LastName = "B", IsManager = false, YearWorked = 2 };

            //Original
            if (emp1 == null)
            {
                throw new AccessViolationException("Employee is null");
            }

            //New nameof
            if (emp1 == null)
            {
                throw new AccessViolationException($"{nameof(Employee)} is null");
            }
        }

        [TestMethod]
        public void Test_CSharp6_ExceptionFilters()
        {
            try
            {
                Customer.DoSomethingFail();
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.Contains("DoSomethingFail"))
                    Console.WriteLine("This is DoSomethingFail");
                else
                    throw;
            }

            // New Exception Filters
            try
            {
                Customer.DoSomethingFail();
            }
            catch (ArgumentException ex) when (ex.Message.Contains("DoSomethingFail"))
            {
                Console.WriteLine("This is DoSomethingFail");
            }
        }

        [TestMethod]
        public void Test_CSharp6_UsingStatic()
        {
            //Original
            var httpStatusCode = System.Net.HttpStatusCode.Accepted;

            //New Using Static
            //using static System.Net.HttpStatusCode;
            var httpStatusCode2 = InternalServerError;
            switch (httpStatusCode2)
            {
                case NotFound:
                    break;
                case OK:
                    break;
                case Created:
                    break;
            }
        }
    }

    public class Point
    {
        //Getter-only auto-properties
        public int X { get; } = 0;
        //Initializers for auto-properties
        public int Y { get; } = 5;
        public Point(int x, int y) => (X, Y) = (x, y);
      
        public double Dist => Math.Sqrt(X * X + Y * Y);
        public override string ToString()
        {
            //String interpolation
            return $"({X},{Y})";
        }

        //Expression-bodied methods
        public string Display() => $"({X},{Y})";
    }

}
