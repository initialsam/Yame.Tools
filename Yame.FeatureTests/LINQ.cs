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

            /*���O
            int[] source = new int[] { 7, 5, 1, 6, 8, 3 };
            Average �T�ؤ�k => foreach���ܤƫ�
            TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)

            Func<TSource, TSource, TSource> func => (total, next) => total + next
            �Ĥ@���] total�O0 , next�O7 �M��^�� 0+7
            �ĤG���] total�O7 , next�O5 �M��^�� 7+5
            �ĤT���] total�O12, next�O1 �M��^�� 12+1
            ....

            TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
            TAccumulate seed = 0;
            Func<TAccumulate, TSource, TAccumulate> func => (count, next) => ++count
            �Ĥ@���] count�O0 , next�O7 �M��^�� count+1
            �ĤG���] count�O1 , next�O5 �M��^�� count+1
            �ĤT���] count�O2 , next�O1 �M��^�� count+1
            ....

            TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
            TAccumulate seed = 0;
            Func<TAccumulate, TSource, TAccumulate> func =>  (total, next) => {total = total + next;totalCount++;return total;},
            Func<TAccumulate, TResult> resultSelector => total => (double)total / totalCount
            �Ĥ@���] total�O0 ,totalCount�O0 , next�O7 �M��^�� 0+7 
            �ĤG���] total�O7 ,totalCount�O1 , next�O5 �M��^�� 7+5
            �ĤT���] total�O12,totalCount�O2 , next�O1 �M��^�� 12+1
            �]����A�h���� resultSelector total�O30 ,totalCount�O6 ,�M��^�� 30/6
             */


        }
        [TestMethod]
        public void Test_LINQ_SetOperations()
        {
            IEnumerable<int> firstSequence = "1,2,3,4,5".Split(',').Select(x => int.Parse(x));
            IEnumerable<int> secondSequence = "5,6".Split(',').Select(x => int.Parse(x));
            IEnumerable<int> concat = firstSequence.Concat(secondSequence);
            Display(concat, "Concat results");
            // 1,2,3,4,5,5,6 Concat results  (�������Ӷ��X�[�_��)

            IEnumerable<int> union = firstSequence.Union(secondSequence);
            Display(union, "Union results");
            //1,2,3,4,5,6 Union results (���Ӷ��X�[�_�� ��������)

            IEnumerable<int> intersect = firstSequence.Intersect(secondSequence);
            Display(intersect, "Intersect results");
            //5 Intersect results (���Ӷ��X�[�_�� ������)

            IEnumerable<int> except = firstSequence.Except(secondSequence);
            Display(except, "Except results");
            //1,2,3,4 Except results (�Ĥ@�Ӷ��X�����e �ư� �ĤG�Ӷ��X�����e �ҳѤU��)

            string[] names = { "Sarah", "Gentry", "Amrit" };
            IEnumerable<string> namesResult = names.Except(new[] { "Sarah", "Amrit", "Mark" });
            Display(namesResult, "String Except results");
            //Gentry String Except results

            void Display<T>(IEnumerable<T> items, string title)
            {
                Console.WriteLine($"{string.Join(",", items)} {title}");
            }
        }


        [TestMethod]
        public void Test_LINQ_2()
        {
            //ref https://github.com/ssukhpinder/LINQExample/blob/master/LINQExample/Program.cs
            string query = "Wikipedia is an online free-content encyclopedia project that aims to help" +
              " create a world in which everyone can freely share in the sum of all knowledge. It is supported by" +
              " the Wikimedia Foundation and based on a model of openly editable content. The name Wikipedia is a" +
              " blending of the words wiki and encyclopedia. Wikipedia articles provide links designed to guide" +
              " the user to related pages with additional information.";

            string searchString = "wikipedia";
            NumberOfWords(searchString);

            string[] wordsToMatch = { "Wikipedia", "wiki", "encyclopedia" };
            GetSentenceWithMatchingWords(wordsToMatch);

            string inputString = "Wikipedia124542Search";
            CountDigits(inputString);

            List<string> inputStrings = new List<string>();
            inputStrings.Add("Visual C#");
            inputStrings.Add("Visual C");
            inputStrings.Add("Visual Node");
            inputStrings.Add("Visual F#");
            RegexExample(inputStrings);

            void CountDigits(string inputString)
            {
                IEnumerable<char> stringQuery =
                  from ch in inputString
                  where Char.IsDigit(ch)
                  select ch;

                Console.WriteLine(string.Join(" ", stringQuery));

                int count = stringQuery.Count();
                Console.WriteLine("Count = {0}", count);
            }

            void GetSentenceWithMatchingWords(string[] wordsToMatch)
            {
                string[] sentences = query.Split(new char[] { '.', '?', '!' });

                var result = from sentence in sentences
                             let w = sentence.Split(
                                 new char[] { '.', ' ', ',' },
                                     StringSplitOptions.RemoveEmptyEntries
                                 )
                             where w.Distinct()
                                    .Intersect(wordsToMatch).Count() == wordsToMatch.Count()
                             select sentence;

                foreach (string sentence in result)
                {
                    Console.WriteLine(sentence.Trim());
                }
            }
            void NumberOfWords(string searchString)
            {

                string[] source = query.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);

                var result = from words in source
                             where words.ToLowerInvariant() == searchString.ToLowerInvariant()
                             select words;

                int wordCount = result.Count();
                Console.WriteLine("{0} occurrences(s) were found.", wordCount);
            }

            void RegexExample(List<string> inputStrings)
            {
                System.Text.RegularExpressions.Regex searchTerm =
                    new System.Text.RegularExpressions.Regex(@"Visual (F#|C#)");

                var matchedStrings =
                    from inputString in inputStrings
                    let matches = searchTerm.Matches(inputString)
                    where matches.Count > 0
                    select new
                    {
                        name = inputStrings,
                        matchedValues = from System.Text.RegularExpressions.Match
                                        match in matches
                                        select match.Value
                    };

                foreach (var v in matchedStrings)
                {
                    Console.WriteLine(v.matchedValues.FirstOrDefault());
                }
            }
        }
    }

}
