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
    public class FuncDelegates
    {
        [TestMethod]
        public void Test_FuncDelegates()
        {
            Func<int, int> addOne = n => n + 1;
            Func<int, int, int> addNums = (x, y) => x + y;
            Func<int, bool> isZero = n => n == 0;

            Console.WriteLine(addOne(5)); // 6
            Console.WriteLine(isZero(addNums(-5, 5))); // True

            int[] a = { 0, 1, 0, 3, 4, 0 };
            Console.WriteLine(a.Count(isZero)); // 3
        }

        [TestMethod]
        public void Test_FuncDelegates2()
        {
            bool[] bools = { false, true, false, false };

            // int IEnumerable.Count<T>(Func<T, Bool> predicate)

            int f = bools.Count(bln => bln == false);

            Console.WriteLine(f); //3

            int t = bools.Count(bln => bln == true);

            Console.WriteLine(t); //1
        }


    }

}
