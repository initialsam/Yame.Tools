using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A00_Basis
{
    public class A00_BasisDemo
    {
        public void Demo1()
        {
            var i = 5;
            var o = new Order()
            {
                ID = 9,
                Name = 'a'
            };

            RefMethod(ref o, ref i);
            Console.WriteLine(i);
            Console.WriteLine(o.ID);
            Console.WriteLine(o.Name);
        }

        private void Method(Order no, int ni)
        {
            ni = 6;
            no.ID = 10;
            no.Name = 'b';

        }

        private void RefMethod(ref Order ro, ref int ri)
        {
            ri = 7;
            ro.ID = 11;
            ro.Name = 'c';
        }

        public class Order
        {
            public int ID { get; set; }
            public char Name { get; set; }
        }
    }
}
