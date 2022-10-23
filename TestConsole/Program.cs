using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace TestConsole
{

    class Program
    {
        static void Main(string[] args)
        {
            var a = new Employee();
            var b = a.GetType();
        }

        class Employee:System.Object
        {
            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }
        }


        private static int NewMethod2()
        {
            return 5 * 5;
        }

        static void 廚房()
        {
            try
            {
                廁所();
            }
            catch (Exception ex)
            {
                throw new Exception("在廚房看到小強但找不到", ex);
            }
        }
        static void 廁所()
        {
            throw new Exception("廁所 出事啦");
        }

        private static void NewMethod1()
        {
            var list = new List<Product>
            {
                new Product{
                    Id=1,
                    Tags=new List<Tag>
                    {
                        new Tag{Id=61,Name="TA"},
                        new Tag{Id=62, Name="TB" },
                        new Tag{Id=63,Name="TC"}
                    },
                    Title="PA",
                    Price=100},
            };
        }

        private static void NewMethod()
        {
            var operatingUnitName = "陳俊欽-紅不讓手機配件-鳳山店";
            var index = operatingUnitName.IndexOf("-") + 1;
            var storeName = operatingUnitName.Substring(index, operatingUnitName.Length - index);

            var v1 = 0 / 2;
            var v2 = 1 / 2;
            var v3 = 2 / 2;

            var aa = new List<string>() { "a", "b", "c", "d", "e", "f", "g" };
            var a = aa.Select((item, inx) => new { item, inx })
                      .GroupBy(x => x.inx / 2);


            var myStackQueue = new StackQueue<int>(); //T is now int

            myStackQueue.Enqueue(1);
            myStackQueue.Push(2);
            myStackQueue.Push(3);
            myStackQueue.Enqueue(4);

            //At this point, the collection is { 3, 2, 1, 4 }

            foreach (var item in myStackQueue)
            {
                Console.WriteLine(item);
            }
        }

        public class StackQueue<T> : IEnumerable<T>
        {
            private List<T> elements = new List<T>();
            private int _top = 0; //NEW

            public void Enqueue(T item)
            {
                Console.WriteLine("Queueing " + item.ToString());
                elements.Insert(elements.Count, item);
                _top++; //NEW
            }

            public void Push(T item)
            {
                Console.WriteLine("Pushing " + item.ToString());
                elements.Insert(0, item);
                _top++; //NEW
            }

            public T Pop()
            {
                var element = elements[0];
                Console.WriteLine("Popping " + element.ToString());
                elements.RemoveAt(0);
                _top--; //NEW
                return element;

            }

            public IEnumerator<T> GetEnumerator()
            {
                for (int index = 0; index < _top; index++)
                {
                    yield return elements[index];
                }
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

    }

    [DebuggerDisplay("{DD}")]
    [DebuggerTypeProxy(typeof(TagDebugView))]
    public class Product
    {
        public int Id { get; set; }
        public List<Tag> Tags { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        private class TagDebugView
        {
            private readonly Product _product;
            public TagDebugView(Product product)
            {
                _product = product;
            }
            [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
            public string TDV => String.Join("，",_product.Tags.Select(x => x.Name));
        }
        private string DD
        {
            get { return $"{Id} {Title} {Price} Tags:{Tags.Count}"; }
        }
        public override string ToString()
        {
            return $"{Title} - {Price}";
        }
    }
    [DebuggerDisplay("Tag : {Id} {Name,nq}")]
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
