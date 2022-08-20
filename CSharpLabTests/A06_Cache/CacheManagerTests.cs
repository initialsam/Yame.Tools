using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpLab.A06_Cache;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CSharpLab.A06_Cache.Tests
{
    [TestClass()]
    public class CacheManagerTests
    {
        [TestMethod()]
        public void GetCachableDataTest()
        {
            var tasks = new List<Task>();
            for (var i = 0; i < 2; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    var data = CacheManager.GetCachableData<string>("KEY",
                        () =>
                        {
                            Console.WriteLine("Thread {0} Start Job",
                                Thread.CurrentThread.ManagedThreadId);
                            Thread.Sleep(3000);
                            Console.WriteLine("Thread {0} Stop Job",
                                Thread.CurrentThread.ManagedThreadId);
                            return "OK";
                        }, 10);
                    Console.WriteLine("Data:" + data);
                }));
            }
            tasks.ForEach(t => t.Wait());

            Console.WriteLine("Done");
            
        }
    }
}