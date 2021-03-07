using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Yame.FeatureTests.Dto;

namespace Yame.FeatureTests
{
    /// <summary>
    /// Ref 
    /// https://www.youtube.com/watch?v=E_p8H3SVCtY
    /// https://www.youtube.com/watch?v=VIGF2JsTKxY
    /// </summary>
    [TestClass]
    public class CSharp7
    {
        [TestMethod]
        public void Test_CSharp7_OutVariables()
        {
            var ageText = "9";
            var expected = 9;
            //Original
            int age = 0;
            var isInt = int.TryParse(ageText, out age);
            age.Should().Be(expected);
            //New Out Variables
            var isInt2 = int.TryParse(ageText, out int age2);
            age2.Should().Be(expected);
        }

        [TestMethod]
        public void Test_CSharp7_PatternMatching()
        {
            var expected = 9;
            //Original
            object ageVal = 9;
            if (ageVal is int)
            {
                var age = (int)ageVal;
                age.Should().Be(expected);
            }
            //New Pattern Matching
            if (ageVal is int age2)
            {
                age2.Should().Be(expected);
            }
            //Out Variables + Pattern Matching
            object ageVal2 = "9";
            if (ageVal2 is int age3 || (ageVal2 is string ageText2 && int.TryParse(ageText2, out age3)))
            {
                age3.Should().Be(expected);
            }
        }

        [TestMethod]
        public void Test_CSharp7_SwitchStatements()
        {
            var emp1 = new Employee { FirstName = "A", LastName = "B", IsManager = false, YearWorked = 2 };
            var emp2 = new Employee { FirstName = "C", LastName = "D", IsManager = true, YearWorked = 28 };
            var cust1 = new Customer { FirstName = "E", LastName = "F", IsVip = false, Age = 30 };
            var cust2 = new Customer { FirstName = "G", LastName = "H", IsVip = true, Age = 50 };
            List<object> people = new List<object> { emp1, emp2, cust1, cust2 };

            //New Switch Statements + Pattern Matching
            foreach (var p in people)
            {
                switch (p)
                {
                    case Employee e when (e.IsManager == false):
                        "A".Should().Be(e.FirstName);
                        break;
                    case Employee e when (e.IsManager):
                        "C".Should().Be(e.FirstName);
                        break;
                    case Customer e when (e.IsVip && e.Age > 40):
                        "G".Should().Be(e.FirstName);
                        break;
                    case Customer e:
                        "E".Should().Be(e.FirstName);
                        break;
                    default:
                        break;
                }
            }

        }

        [TestMethod]
        public void Test_CSharp7_ThrowExpression()
        {
            var emp1 = new Employee { FirstName = "A", LastName = "B", IsManager = false, YearWorked = 2 };
            var emp2 = new Employee { FirstName = "C", LastName = "D", IsManager = true, YearWorked = 28 };
            var people = new List<Employee> { emp1, emp2 };
            //Original
            var ceo = people.Where(x => x.IsManager).FirstOrDefault();
            if (ceo == null) throw new Exception("There was a problem finding a manager");
            //New
            var ceo2 = people.Where(x => x.IsManager).FirstOrDefault() ?? throw new Exception("There was a problem finding a manager");

        }

        [TestMethod]
        public void Test_CSharp7_Tuples()
        {
            //Tuples
            var stuff = (name: "bar", age: 42);
            "bar".Should().Be(stuff.name);
            42.Should().Be(stuff.age);
            //7.1
            var name = "bar";
            var age = 42;
            var p = (name, age);
            "bar".Should().Be(p.name);
            42.Should().Be(p.age);

            //Local Functions + Tuples
            var po = LocalFunctions();
            "Sam".Should().Be(po.firstName);
            "Chen".Should().Be(po.lastName);

            (string firstName, string lastName) LocalFunctions()
            {
                return Helper.SplitName("Sam Chen");
            }
        }

        [TestMethod]
        public void Test_CSharp7_Discards()
        {
            var emp = new Employee("John", "Quincy");
            //Original
            if (emp == null)
            {
                throw new ArgumentException("emp is null");
            }
            //New
            _ = emp ?? throw new ArgumentException("emp is null");

            //Tuples + Discards
            var (_, lastName) = Helper.SplitName("Sam Chen");
            "Chen".Should().Be(lastName);

            //Deconstruct
            var (fName, lName, _, _) = emp;
            "John".Should().Be(fName);
            "Quincy".Should().Be(lName);

        }

        [TestMethod]
        public void Test_CSharp7_Discards2()
        {
            //ref https://github.com/ssukhpinder/DiscardExample/blob/master/DiscardExample/Program.cs
            var emp = new Employee("Tim", "Lee");
            
            emp.ShowValue("Show time");
            var (_, _, _, pop1, _, pop2) = emp.QueryCityPopulationByYear("India", 1947, 2020);
            Console.WriteLine($"Population chage: {pop2 - pop1:N0}");
        }
    }
}
