﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchmarkDotnetNote
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<VS>();
        }
    }
    /// <summary>
    /// 測試用擂台
    /// </summary>
    public class VS
    {
        // 紅方選手
        [Benchmark]
        public void add1()
        {
            string[] array1 = new string[] { "a", "b", "c", "d", "e" };
            string[] array2 = new string[] { "a", "x", "b", "c", "d", "y", "e" };
            add1(array1, array2);//ax xb dy ye


            array2 = new string[] { "x", "a", "b", "c", "d", "y", "e" };
            add1(array1, array2);//xa dy ye

            array2 = new string[] { "a", "y", "b", "c", "d", "e", "z" };
            add1(array1, array2);// ay yb ez
        }

        // 藍方選手
        [Benchmark]
        public void add2()
        {
            string[] array1 = new string[] { "a", "b", "c", "d", "e" };
            string[] array2 = new string[] { "a", "x", "b", "c", "d", "y", "e" };
            add2(array1, array2);//ax xb dy ye


            array2 = new string[] { "x", "a", "b", "c", "d", "y", "e" };
            add2(array1, array2);//xa dy ye

            array2 = new string[] { "a", "y", "b", "c", "d", "e", "z" };
            add2(array1, array2);// ay yb ez
        }
        HashSet<string> add2(string[] array1, string[] array2)
        {
            HashSet<string> r = new HashSet<string>();
            HashSet<string> a = new HashSet<string>();
            if (array1.Length == array2.Length)
            {
                r.Add("Same");
                return r;
            }
            for (int i = 0; i < array1.Length - 1; i++)
            {
                string current = array1[i];
                string next = array1[i + 1];
                r.Add($"{current}{next}");
            }
            for (int i = 0; i < array2.Length - 1; i++)
            {
                string current = array2[i];
                string next = array2[i + 1];
                if (r.Contains($"{current}{next}") == false)
                {
                    a.Add($"{current}{next}");
                };
            }
            return a;
        }
        HashSet<string> add1(string[] array1, string[] array2)
        {
            HashSet<string> r = new HashSet<string>();
            if (array1.Length == array2.Length)
            {
                r.Add("Same");
                return r;
            }
            //先只會有加入一個英文到array2

            var newList = array2.Except(array1).ToList();
            foreach (var result in newList)
            {
                int array2index = Array.IndexOf(array2, result);

                if (array2index == 0)
                {
                    r.Add($"{result}{array2[array2index + 1]}");
                    //Console.WriteLine($"Element {result} found before {array2[array2index + 1]}");
                    //Console.WriteLine($"Element {result} found before {array2[array2index + 1]}");
                }
                else if (array2index == array2.Length - 1)
                {
                    r.Add($"{array2[array2index - 1]}{result}");
                    //Console.WriteLine($"Element {result} found after {array2[array2index - 1]}");
                }
                else
                {
                    r.Add($"{array2[array2index - 1]}{result}");
                    r.Add($"{result}{array2[array2index + 1]}");
                    //Console.WriteLine($"Element {result} found between {array2[array2index - 1]} and {array2[array2index + 1]}");
                }

            }

            return r;
        }
    }
  
}