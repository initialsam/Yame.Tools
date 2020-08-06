using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A03_Constructor
{
    public class A03_Constructor_Demo
    {
        #region --繼承Demo--
        public void 繼承Demo()
        {
            Console.WriteLine("---------------");
            Console.WriteLine("---繼承Demo---");
            Console.WriteLine("---------------");
            var a = new A();
            Console.WriteLine("----------------");
            var b = new B();
            Console.WriteLine("----------------");
            var C = new C();
            Console.WriteLine("----------------");
            /*
                A 建構子
                ----------------
                A 建構子
                B 建構子
                ----------------
                A 建構子
                B 建構子
                C 建構子
                ----------------
            */
        }
        public class A
        {
            public A()
            {
                Console.WriteLine("A 建構子");
            }
        }

        public class B : A
        {
            public B()
            {
                Console.WriteLine("B 建構子");
            }
        }

        public class C : B
        {
            public C()
            {
                Console.WriteLine("C 建構子");
            }
        }
        #endregion

        public void thisDemo()
        {
            Console.WriteLine("---------------");
            Console.WriteLine("---this Demo---");
            Console.WriteLine("---------------");
            var d = new D();
            Console.WriteLine("----------------");
            /*
                D 建構子 ,x = 無參數 ,y = 87
                D 建構子 ,x = 無參數
                D 建構子
                ----------------
            */

            var dd = new D("測試");
            Console.WriteLine("----------------");
            /*
                D 建構子, x = 測試, y = 87
                D 建構子, x = 測試
            */
        }

        public void baseDemo()
        {
            Console.WriteLine("---------------");
            Console.WriteLine("---base Demo---");
            Console.WriteLine("---------------");
            var e = new E();
            /*
                D 建構子 ,x = 無參數 ,y = 87
                D 建構子, x = 無參數
                D 建構子
                E 建構子
            */
            Console.WriteLine("----------------");
            var ee = new E("參數");
            /*
                D 建構子 ,x = 參數 ,y = 87
                D 建構子, x = 參數
                E 建構子, x = 參數
            */
            Console.WriteLine("----------------");

            var eee = new E("參數",55);
            /*
                D 建構子 ,x = 參數 ,y = 55
                E 建構子 ,x = 參數 ,y = 55
            */
            Console.WriteLine("----------------");


        }
        public class D
        {
            public D() : this("無參數")
            {
                Console.WriteLine("D 建構子");
            }

            public D(string x) : this(x,87)
            {
                Console.WriteLine("D 建構子 ,x = " + x);
            }

            public D(string x,int y)
            {
                Console.WriteLine($"D 建構子 ,x = {x} ,y = {y}");
            }
        }

        public class E:D
        {
            public E()
            {
                Console.WriteLine("E 建構子");
            }

            public E(string x) : base(x)
            {
                Console.WriteLine("E 建構子 ,x = " + x);
            }

            public E(string x, int y) : base(x,y)
            {
                Console.WriteLine($"E 建構子 ,x = {x} ,y = {y}");
            }
        }
    }

  

}
