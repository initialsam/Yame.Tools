using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A08_Attributes
{
    [DebuggerDisplay("Name={Name}，Count={Count}")]
    [DebuggerTypeProxy(typeof(ProductDebugDisplay))]
    [DefaultColor(Color = System.ConsoleColor.Green)]
    public class ProductB
    {
        [Display("Name: ", System.ConsoleColor.Cyan)]
        [Indent(count:8)]
        [Indent]
        public string Name { get; set; }
        public int Count { get; set; }
    }


    public class ProductA
    {
        public string Name { get; set; }

        public int Count { get; set; }
    }
}
