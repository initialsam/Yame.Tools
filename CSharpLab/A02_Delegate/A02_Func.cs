using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A02_Delegate
{
    public class A02_Func
    {
        //public delegate string MyDelegate(int x);
        //public delegate string Func(int x);
        //public Func<int, string> doo;

        public void Demo3()
        {
            //Func doo = Method;
            Func<int, string> doo = Method;
            string result = doo(5);
        }

        public string Method(int x)
        {
            var temp = x.ToString();
            return temp;
        }

        public void Demo()
        {
            //BookFilter.Filter myFilter = CheapBookFilter;
            BookFilterUseFunc myBookFilterUseFunc = new BookFilterUseFunc();
            var books = myBookFilterUseFunc.GetBooks(CheapBookFilter);
        }

        public List<Book> CheapBookFilter(List<Book> bookList)
        {
            return bookList.Where(x => x.Price < 250).ToList();
        }
    }

    public class BookFilterUseFunc
    {
        //public delegate List<Book> Filter(List<Book> bookList);
        public List<Book> GetBooks(Func<List<Book>, List<Book>> myFilter)
        {
            List<Book> bookList = BookHelper.GetBookList();
            if (myFilter == null)
            {
                return bookList;
            }
            return myFilter(bookList);
        }
    }
}
