using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacNote.Infrastructure
{
    internal class Singleton2
    {
        private Guid UniqueKey = Guid.NewGuid();

        //讓Autofac 來處理 單例模式
        //private static Singleton2 instance = null;
        //public static Singleton2 Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new Singleto2n();
        //        }
        //        return instance;
        //    }
        //}

        /// <summary>
        /// 建構式
        /// </summary>
        public Singleton2()
        {
            Console.WriteLine("執行 Singleton2 建構式");
        }

        public void ShowUniqueKey()
        {
            Console.WriteLine($"Singleton2 Unique Key = {UniqueKey}");
        }
    }
}
