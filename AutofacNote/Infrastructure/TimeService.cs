using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacNote.Infrastructure
{
    internal class TimeService: ITimeService
    {
        private Guid UniqueKey = Guid.NewGuid();

        public TimeService(Singleton2 singleton2)
        {
            Console.WriteLine("執行 TimeService 建構式");
            singleton2.ShowUniqueKey();
            Console.WriteLine("結束 TimeService 建構式");

        }
        public void GetNow()
        {
            Console.WriteLine($"現在時間 {DateTime.Now}");
        }

        public void ShowUniqueKey()
        {
            Console.WriteLine($"Singleton2 Unique Key = {UniqueKey}");
        }
    }
}
