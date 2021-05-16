using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A09_Reflection
{
    public class ProductDto
    {
        public double Price { get; set; }
        public bool EnableProduct { get; set; }
        public DateTime CreateDate { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
