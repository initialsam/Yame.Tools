using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A02_Delegate
{
    public class A02_LambdaExpression
    {
        //public delegate string MyDelegate(int x);
        //public delegate string Func(int x);
        //public Func<int, string> doo;

        public void Demo3()
        {
            //Func<int, string> doo = Method;
            // 匿名方法
            Func<int, string> doo1 = delegate (int x) 
            {
                var temp = x.ToString();
                return temp;
            };
            
            //Lambda Expression
            //陳述式 Lambda (有大括號 可多行)
            Func<int, string> doo3 = (int x) => 
            {
                var temp = x.ToString();
                return temp;
            };

            Func<int, string> doo2 = delegate (int x) { return x.ToString(); };
            //陳述式 Lambda
            Func<int, string> doo4 = (int x) => { return x.ToString(); };
            //運算式 Lambda
            Func<int, string> doo5 = (int x) => x.ToString();
            Func<int, string> doo6 = x => x.ToString();

            string result = doo1(5);
        }

        public void Demo()
        {
            BookFilterUseLambda myBookFilterUseFunc = new BookFilterUseLambda();
            var books = myBookFilterUseFunc.GetBooks(
                bookList=> bookList.Where(x => x.Price < 250).ToList());
        }

        //public List<Book> CheapBookFilter(List<Book> bookList)
        //{
        //    return bookList.Where(x => x.Price < 250).ToList();
        //}
    }

    public class BookFilterUseLambda
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

 

