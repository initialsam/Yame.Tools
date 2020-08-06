using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A02_Delegate
{
    public class BookFilter
    {
        public delegate List<Book> Filter(List<Book> bookList);

        public List<Book> GetBooks(Filter myFilter)
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
