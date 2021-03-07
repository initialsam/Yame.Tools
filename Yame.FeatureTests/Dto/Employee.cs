using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.FeatureTests.Dto
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsManager { get; set; }
        public int YearWorked { get; set; }

        public Employee(){}
        public Employee(string firstName, string lastName) =>
            (FirstName, LastName) = (firstName, lastName);
        
        public void Deconstruct(out string firstName, 
                                out string lastName,
                                out bool isManager,
                                out int yearWorked)
        {
            firstName = FirstName;
            lastName = LastName;
            isManager = IsManager;
            yearWorked = YearWorked;
        }
        /// <summary>
        /// _ 就是一個變數 只是想要讓你不去在意他的存在
        /// </summary>
        /// <param name="_"></param>
        public void ShowValue(string _)
        {
            Console.WriteLine(_);
            _ = "Hello World";
            Console.WriteLine(_);
        }

        public (string, double, int, int, int, int) QueryCityPopulationByYear(string name, int year1, int year2)
        {
            int pop1 = 0, pop2 = 0;
            double area = 0;
            if (name == "India")
            {
                area = 468.48;
                if (year1 == 1947)
                {
                    pop1 = 7781984;
                }
                if (year2 == 2020)
                {
                    pop2 = 8175133;
                }
                return (name, area, year1, pop1, year2, pop2);
            }
            //Return nothing
            return ("", 0, 0, 0, 0, 0);
        }
    }
}
