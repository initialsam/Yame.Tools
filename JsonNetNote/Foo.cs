using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonNetNote
{
    public class Foo
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsTrue { get; set; }
        public DateTime Today { get; set; }
        public DateTime? TodayCanNull { get; set; }
        public decimal point { get; set; }

        public Person Partner { get; set; }
        
    }
}
