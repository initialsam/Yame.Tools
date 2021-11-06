using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonNetNote
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person Partner { get; set; }
        public decimal? Salary { get; set; }
    }
}
