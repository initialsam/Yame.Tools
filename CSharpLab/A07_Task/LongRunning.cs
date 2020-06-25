using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpLab.A07_Task
{
    //http://blog.i3arnon.com/2015/07/02/task-run-long-running/
    public class LongRunning
    {
        public void demo()
        {
            var taskFactory = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    FooAsync();
                    Task.Delay(TimeSpan.FromSeconds(2));
                    Console.WriteLine("Is background thread: {0}", Thread.CurrentThread.IsBackground);
                    Console.WriteLine("Is threadpool thread: {0}", Thread.CurrentThread.IsThreadPoolThread);
                }
            }, TaskCreationOptions.LongRunning);

            //Task actualTask = taskFactory.Unwrap();
            taskFactory.Wait();

            Task task = Task<int>.Run(async () =>
            {
                while (true)
                {
                    await FooAsync();
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    Console.WriteLine("Is background thread: {0}", Thread.CurrentThread.IsBackground);
                    Console.WriteLine("Is threadpool thread: {0}", Thread.CurrentThread.IsThreadPoolThread);
                }
            });

            task.Wait();
        }

        private async Task<int> FooAsync()
        {
            return 1;
            //throw new NotImplementedException();
        }
    }
}
