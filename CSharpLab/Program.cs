using CSharpLab.A00_Basis;
using CSharpLab.A01_Generics;
using CSharpLab.A01_Generics.Covariance;
using CSharpLab.A02_Delegate;
using CSharpLab.A03_Constructor;
using CSharpLab.A05_Enum;
using CSharpLab.A06_Cache;
using CSharpLab.A07_Task;
using CSharpLab.A08_Attributes;
using CSharpLab.A09_Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab
{
    class Program
    {
        static void Main(string[] args)
        {
            //A00_Basis();
            //A01_Generics();
            //A02_Delegate();
            //A02_Func();
            //A02_LambdaExpression();
            //A03_Constructor_Demo();
            //A05_Enum_Demo();
            //A06_Cache_Demo();
            //A07_Task_Demo();
            //A08_Attributes_Demo();
            A09_Reflection_Demo();
            //var container = new MinimalContainer();
            //container.Register<IWelcomer, Welcomer>();
            //container.Register<IWriter, ConsoleWriter>();
            //var welcomer = container.Create<IWelcomer>();
            //welcomer.SayHelloTo("World");
        }

        private static bool aaa(Book x)
        {
            return x.OnSell == false;
        }


        private static List<Book> NewMethod()
        {
            var b = BookHelper.GetBookList();

            return b.OrderBy(x => x.Price).ToList();
        }

        private static void A00_Basis()
        {
            var a = new A00_BasisDemo();
            a.Demo1();
        }

        private static void A01_Generics()
        {
            var a = new APIData<Product, int>()
            {
                Version = "V1",
                ErrorCode = 0,
                Data = new Product()
                {
                    ID = 87,
                    Name = "產品"
                }
            };
            var cc = JsonHelper.ToJson(a);

            var c = JsonHelper.JsonToObject<APIData<Product, int>>("a");

            var bb = new A01B_Covariance();
            bb.testCovariant();
            bb.testContravariant();

        }

        private static void A02_Delegate()
        {
            var a = new DelegateDemo();
            a.Demo();
        }
        private static void A02_Func()
        {
            var a = new A02_Func();
            a.Demo();
        }

        private static void A02_LambdaExpression()
        {
            var a = new A02_LambdaExpression();
            a.Demo();
        }

        private static void A03_Constructor_Demo()
        {
            var a = new A03_Constructor_Demo();
            a.繼承Demo();
            a.thisDemo();
            a.baseDemo();
        }

        private static void A05_Enum_Demo()
        {
            var a = new A06_Enum_Demo();
            a.Demo();
        }

        private static void A06_Cache_Demo()
        {
            var a = new A06_Cache_Demo();
            a.Demo();
        }

        private static void A07_Task_Demo()
        {
            var a = new A07_Task_Demo();
            a.Demo();
        }

        private static void A08_Attributes_Demo()
        {
            var a = new A08_Attributes_Demo();
            a.Demo();
        }
        private static void A09_Reflection_Demo()
        {
            LogHelper.Delimiter = "\r\n";
            LogHelper.ArrowSymbol = "=>";
            var a = new A09_Reflection_Demo();
            a.Demo1();
            a.Demo2();
            a.Demo3();
        }
        
    }

    public class Student
    {
        public string Name { get; set; }
        public List<Course> Course { get; set; }
    }

    public class Course
    {
        public string CourseName { get; set; }
    }
}
