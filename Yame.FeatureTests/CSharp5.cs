using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.HttpStatusCode;
using Yame.FeatureTests.Dto;
using System.Threading.Tasks;

namespace Yame.FeatureTests
{
    [TestClass]
    public class CSharp5
    {
        [TestMethod]
        public void Test_CSharp5_BlockThread()
        {
            Task task = MyPromiseAsync();
            Console.WriteLine("1");
            task.Wait();    
            //主thread 跟 背景thread 都會被佔住 , 主thread 不做事 等 背景thread做完後 接續
            Console.WriteLine("2");
            // 3 4 1 5 2
        }

        [TestMethod]
        public async Task Test_CSharp5_Await()
        {
            Task task = MyPromiseAsync();
            Console.WriteLine("1");
            await task.ConfigureAwait(false);
            //主thread 去服務別人了 背景thread去做事 做完後, 不需要原主thread接續
            Console.WriteLine("2");

            // 3 4 1 5 2
        }
        [TestMethod]
        public async Task Test_CSharp5_TaskDoAgain()
        {
            Task task = MyPromiseAsync();
            Console.WriteLine("1");
            task.Wait();
            Console.WriteLine("2");
            await task.ConfigureAwait(false);
            Console.WriteLine("A");
            // 3 4 1 5 2 A
            //因為已經執行過的task會把結果存起來 不會再次執行
            //所以WhenAll跑完後 可以再去拿結果
        }

        public static async Task MyPromiseAsync()
        {
            Console.WriteLine("3");
            await Task.FromResult("Result");
            Console.WriteLine("4");
            await Task.Delay(1000);
            Console.WriteLine("5");
        }

    }

}
