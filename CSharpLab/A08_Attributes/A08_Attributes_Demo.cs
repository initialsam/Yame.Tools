using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A08_Attributes
{
    public class A08_Attributes_Demo
    {
        public void Demo()
        {
            DemoDebugAttribute();
            DebugInfo();
            YameInfo();
            UseDefaultColor();
            WriteFirstName();
        }

        private static void DemoDebugAttribute()
        {
            var a = new ProductA
            {
                Name = "Sarah",
                Count = 15,
            };
            var b = new ProductB
            {
                Name = "Sarah",
                Count = 16,
            };
            Console.WriteLine("Done");
        }

        [Conditional("DEBUG")]
        public void DebugInfo()
        {
            Console.WriteLine("DEBUG INFO");
        }

        [Conditional("YAME")]
        public void YameInfo()
        {
            Console.WriteLine("YAME INFO");
        }

        private void UseDefaultColor()
        {
            DefaultColorAttribute defaultColorAttribute = (DefaultColorAttribute)
                Attribute.GetCustomAttribute(typeof(ProductB), typeof(DefaultColorAttribute));

            if (defaultColorAttribute != null)
            {
                Console.ForegroundColor = defaultColorAttribute.Color;
            }
            Console.WriteLine("UseDefaultColor");
        }

        private void WriteFirstName()
        {
            PropertyInfo firstNameProperty =
                typeof(ProductB).GetProperty(nameof(ProductB.Name));

            DisplayAttribute firstNameDisplayAttribute = (DisplayAttribute)
                Attribute.GetCustomAttribute(firstNameProperty, typeof(DisplayAttribute));

            IndentAttribute[] indentAttributes = (IndentAttribute[])
                Attribute.GetCustomAttributes(firstNameProperty, typeof(IndentAttribute));


            StringBuilder sb = new StringBuilder();

            if (indentAttributes != null)
            {
                foreach (IndentAttribute a in indentAttributes)
                {
                    sb.Append(new string(' ', 4));
                }
            }

            if (firstNameDisplayAttribute != null)
            {
                Console.ForegroundColor = firstNameDisplayAttribute.Color;
                sb.Append(firstNameDisplayAttribute.Label);
            }
            var b = new ProductB
            {
                Name = "Sarah",
                Count = 16,
            };
            if (b.Name != null)
            {
                sb.Append(b.Name);
            }

            Console.WriteLine(sb);
        }
    }
}
