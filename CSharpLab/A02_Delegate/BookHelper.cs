using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A02_Delegate
{
    public class BookHelper
    {
        public static List<Book> GetBookList()
        {
            return new List<Book>()
            {
                new Book{OnSell = true,Id=1, Category = "商業理財", Name="幸福競爭力",Price=237},
                new Book{OnSell = true,Id=2, Category = "商業理財", Name="最有生產力的一年",Price=300},
                new Book{OnSell = true,Id=3, Category = "商業理財", Name="五線譜投資術",Price=196},
                new Book{OnSell = false,Id=4, Category = "電腦資訊", Name="精通 Python",Price=616},
                new Book{OnSell = false,Id=5, Category = "文學小說", Name="人魚沉睡的家",Price=332},
                new Book{OnSell = false,Id=6, Category = "文學小說", Name="解憂雜貨店",Price=277},
            };
        }
    }
}
