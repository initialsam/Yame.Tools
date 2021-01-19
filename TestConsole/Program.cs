using System;
using System.Collections;
using System.Collections.Generic;
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
            var operatingUnitName = "陳俊欽-紅不讓手機配件-鳳山店";
            var index = operatingUnitName.IndexOf("-")+1;
            var storeName = operatingUnitName.Substring(index, operatingUnitName.Length- index);

            var v1 = 0 / 2;
            var v2 = 1 / 2;
            var v3 = 2 / 2;

            var aa = new List<string>() { "a", "b", "c", "d", "e", "f", "g" };
            var a = aa.Select((item, inx) => new { item, inx })
                      .GroupBy(x => x.inx/2);
        

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
}
