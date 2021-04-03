using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A08_Attributes
{
    //Property 才能用
    [AttributeUsage(AttributeTargets.Property)]
    class DisplayAttribute : Attribute
    {
        public DisplayAttribute(string label, ConsoleColor color = ConsoleColor.White)
        {
            Label = label ?? throw new ArgumentNullException(nameof(label));
            Color = color;
        }

        public string Label { get; }

        public ConsoleColor Color { get; }
    }
}
