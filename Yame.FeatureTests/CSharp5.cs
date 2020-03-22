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
            //�Dthread �� �I��thread ���|�Q���� , �Dthread ������ �� �I��thread������ ����
            Console.WriteLine("2");
            // 3 4 1 5 2
        }

        [TestMethod]
        public async Task Test_CSharp5_Await()
        {
            Task task = MyPromiseAsync();
            Console.WriteLine("1");
            await task.ConfigureAwait(false);
            //�Dthread �h�A�ȧO�H�F �I��thread�h���� ������, ���ݭn��Dthread����
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
            //�]���w�g����L��task�|�⵲�G�s�_�� ���|�A������
            //�ҥHWhenAll�]���� �i�H�A�h�����G
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
