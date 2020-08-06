using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpLab.A07_Task
{
    class LimitNumberOfSimultaneousTasks
    {
        public void demo()
        {
            int workerThreadCount;
            int ioThreadCount;

            ThreadPool.GetMinThreads(out workerThreadCount, out ioThreadCount);

            Console.WriteLine("Default min worker thread: " + workerThreadCount.ToString());
            Console.WriteLine("Default min I/O thread: " + ioThreadCount.ToString());

            ThreadPool.GetMaxThreads(out workerThreadCount, out ioThreadCount);

            Console.WriteLine("Default max worker thread: " + workerThreadCount.ToString());
            Console.WriteLine("Default max I/O thread: " + ioThreadCount.ToString());
        }
    }
}
