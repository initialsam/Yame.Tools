using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpLab.A06_Cache
{
    public class A06_Cache_Demo
    {

        public void Demo()
        {
            var tasks = new List<Task>();
            for (var i = 0; i < 3; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    var data = CacheManager.GetCachableData<string>(
                            key : "KEY",
                            callback : () =>
                                {
                                    var mThreadId = Thread.CurrentThread.ManagedThreadId;
                                    Console.WriteLine("Thread {0} Start Job", mThreadId);
                                    SpinWait.SpinUntil(() => false, 3000);
                                    Console.WriteLine("Thread {0} Stop Job", mThreadId);
                                    return "OK";
                                },
                            cacheMins : 10 ,
                            forceRefresh : false
                        );
                    Console.WriteLine("Data:" + data);
                }));
            }
            tasks.ForEach(t => t.Wait());

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
