using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacNote.Infrastructure
{
    internal class Singleton: ISingleton
    {
        private Guid UniqueKey = Guid.NewGuid();

        //讓Autofac 來處理 單例模式
        //private static Singleton instance = null;
        //public static Singleton Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new Singleton();
        //        }
        //        return instance;
        //    }
        //}

        /// <summary>
        /// 建構式
        /// </summary>
        public Singleton()
        {
            Console.WriteLine("執行 Singleton 建構式");
        }

        public void ShowUniqueKey()
        {
            Console.WriteLine($"Singleton Unique Key = {UniqueKey}");
        }
    }
}
