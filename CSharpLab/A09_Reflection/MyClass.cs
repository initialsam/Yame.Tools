using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A09_Reflection
{
    public interface IMyClass { }

    public class MyClass: IMyClass
    {
        public int PublicProperty { get; set; }
        public int PublicField;
        internal int InternalProperty { get; set; }
        internal int InternalField;
        private int PrivateProperty { get; set; }
        private int PrivateField;

        public void SayHello()
        {
            Console.WriteLine("Hello");
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine($"Message: {message}");
        }

        private void SaySomething(string message)
        {
            Console.WriteLine($"Say something: {message}");
        }
    }
}
