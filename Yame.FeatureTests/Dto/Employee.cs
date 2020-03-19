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
    }
}
