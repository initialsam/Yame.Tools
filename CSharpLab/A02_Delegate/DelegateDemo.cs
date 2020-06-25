using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A02_Delegate
{
    public class DelegateDemo
    {
        public delegate string MyDelegate(int x);

        public void Demo3()
        {
            //MyDelegate doo = new MyDelegate(Method);
            //string result = doo.Invoke(5);
            MyDelegate doo = Method;
            string result = doo(5);
        }

        public string Method(int x)
        {
            var temp = x.ToString();
            return temp;
        }


    public void Demo()
    {
        BookFilter.Filter myFilter = CheapBookFilter;
        BookFilter myBookFilter = new BookFilter();
        var books = myBookFilter.GetBooks(myFilter);
    }

        public List<Book> CheapBookFilter(List<Book> bookList)
        {
            return bookList.Where(x => x.Price < 300).ToList();
        }
    }
}
