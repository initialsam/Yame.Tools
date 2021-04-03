using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpLab.A07_Task
{
    public class A07_Task_Demo
    {
        public void Demo()
        {
            new LimitNumberOfSimultaneousTasks().demo();
            new LongRunning().demo();
            // start new task
            var task = Task<string>.Factory.StartNew(() => {

                Thread.Sleep(2000);
                Console.WriteLine("Hello World");

                #region thread information
                Console.WriteLine ("Is background thread: {0}", Thread.CurrentThread.IsBackground);
                Console.WriteLine ("Is threadpool thread: {0}", Thread.CurrentThread.IsThreadPoolThread);
                #endregion

                #region throw exception
                //throw new InvalidOperationException("Something went wrong");
                #endregion
                return "Mark";
            });

            // wait for task to complete
            task.Wait();

            // use result
            Console.Write("Your name is ");
            Console.Write(task.Result);
        }
    }
}
