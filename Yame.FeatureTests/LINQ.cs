using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.HttpStatusCode;
using Yame.FeatureTests.Dto;
using System.Threading.Tasks;
using System.Text;

namespace Yame.FeatureTests
{
    [TestClass]
    public class LINQ
    {
        [TestMethod]
        public void Test_LINQ_Zip()
        {
            string[] names = { "aa", "bb", "cc" };
            string[] ages = { "1", "2", "3" };
            var list = names.Zip(ages, (name, age) => $"{name}-{age}").ToList();
            Console.WriteLine(string.Join(",", list));
            //aa-1,bb-2,cc-3

            /*
            private static List<string> Join(string[] names, string[] ages)
            {
                List<string> joined = new List<string>();

                for (int i = 0; i < names.Length; i++)
                {
                    joined.Add($"{names[i]},{ages[i]}");
                }

                return joined;
            }
            */
        }
        [TestMethod]
        public void Test_LINQ_Aggregate()
        {
            //Ref https://ithelp.ithome.com.tw/articles/10197334
            int[] source = new int[] { 7, 5, 1, 6, 8, 3 };
            //source.Sum(x => x);
            var sum = source.Aggregate((total, next) => total + next);
            Console.WriteLine($"Sum:{sum}");
            //Sum:30

            //source.Min(x => x);
            var min = source.Aggregate((min, next) => min > next ? next : min);
            Console.WriteLine($"Min:{min}");
            //Min:1

            //source.Count(x => x);
            var count = source.Aggregate(0, (count, next) => ++count);
            Console.WriteLine($"Count:{count}");
            //Count: 6

            //source.Average(x => x);
            var totalCount = 0;
            var average = source.Aggregate(
                            0,
                            (total, next) =>
                            {
                                total = total + next;
                                totalCount++;
                                return total;
                            },
                            total => (double)total / totalCount
                            );
            Console.WriteLine($"Average:{average}");
            //Average:5

            /*筆記
            int[] source = new int[] { 7, 5, 1, 6, 8, 3 };
            Average 三種方法 => foreach的變化型
            TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)

            Func<TSource, TSource, TSource> func => (total, next) => total + next
            第一次跑 total是0 , next是7 然後回傳 0+7
            第二次跑 total是7 , next是5 然後回傳 7+5
            第三次跑 total是12, next是1 然後回傳 12+1
            ....

            TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
            TAccumulate seed = 0;
            Func<TAccumulate, TSource, TAccumulate> func => (count, next) => ++count
            第一次跑 count是0 , next是7 然後回傳 count+1
            第二次跑 count是1 , next是5 然後回傳 count+1
            第三次跑 count是2 , next是1 然後回傳 count+1
            ....

            TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
            TAccumulate seed = 0;
            Func<TAccumulate, TSource, TAccumulate> func =>  (total, next) => {total = total + next;totalCount++;return total;},
            Func<TAccumulate, TResult> resultSelector => total => (double)total / totalCount
            第一次跑 total是0 ,totalCount是0 , next是7 然後回傳 0+7 
            第二次跑 total是7 ,totalCount是1 , next是5 然後回傳 7+5
            第三次跑 total是12,totalCount是2 , next是1 然後回傳 12+1
            跑完後再去執行 resultSelector total是30 ,totalCount是6 ,然後回傳 30/6
             */


        }
        [TestMethod]
        public void Test_LINQ_SetOperations()
        {
            IEnumerable<int> firstSequence = "1,2,3,4,5".Split(',').Select(x => int.Parse(x));
            IEnumerable<int> secondSequence = "5,6".Split(',').Select(x => int.Parse(x));
            IEnumerable<int> concat = firstSequence.Concat(secondSequence);
            Display(concat, "Concat results");
            // 1,2,3,4,5,5,6 Concat results  (直接把兩個集合加起來)

            IEnumerable<int> union = firstSequence.Union(secondSequence);
            Display(union, "Union results");
            //1,2,3,4,5,6 Union results (把兩個集合加起來 取不重複)

            IEnumerable<int> intersect = firstSequence.Intersect(secondSequence);
            Display(intersect, "Intersect results");
            //5 Intersect results (把兩個集合加起來 取重複)

            IEnumerable<int> except = firstSequence.Except(secondSequence);
            Display(except, "Except results");
            //1,2,3,4 Except results (第一個集合的內容 排除 第二個集合的內容 所剩下的)

            string[] names = { "Sarah", "Gentry", "Amrit" };
            IEnumerable<string> namesResult = names.Except(new[] { "Sarah", "Amrit", "Mark" });
            Display(namesResult, "String Except results");
            //Gentry String Except results

            void Display<T>(IEnumerable<T> items, string title)
            {
                Console.WriteLine($"{string.Join(",", items)} {title}");
            }
        }



    }

}
