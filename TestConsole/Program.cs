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
            var aa = new List<string>();
            var bb = aa.Any();
            


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
