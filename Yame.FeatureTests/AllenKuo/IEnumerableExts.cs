using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yame.FeatureTests.AllenKuo
{
    /// <summary>
    /// 格子樑 99.05.08 驗證輸入值 - LINQ, Regex, Func
    /// https://www.evernote.com/shard/s530/client/snv?fbclid=IwAR0tzCY7_n2vq6n6k9xZ_BmgiEsZeo4hk51O5kxE7ttK8rTZzKwWJa6_nEg&noteGuid=5425159e-92f8-4673-a312-e0257047b8bd&noteKey=cb560cd267ff6b49f18016ac63e94786&sn=https%3A%2F%2Fwww.evernote.com%2Fshard%2Fs530%2Fsh%2F5425159e-92f8-4673-a312-e0257047b8bd%2Fcb560cd267ff6b49f18016ac63e94786&title=99.05.08%2B%25E9%25A9%2597%25E8%25AD%2589%25E8%25BC%25B8%25E5%2585%25A5%25E5%2580%25BC%2B-%2BLINQ%252C%2BRegex%252C%2BFunc
    /// </summary>
    public static class IEnumerableExts
    {
        /// <summary>
        /// 判斷結果為 true的次數若大於或等於指定次數,就傳回true,不再向下執行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static bool TrueGreaterOrEqualThan<T>(this IEnumerable<Func<T, bool>> predicates, T value, int times)
        {
            int count = 0;
            foreach (var func in predicates)
            {
                if (func(value))
                {
                    count++;
                }

                if (count >= times)
                {
                    return true;
                }
            }

            return false;
        }
    }
    public static class StringExts
    {
        public static bool IsMatch(this string source, string pattern)
        {
            return Regex.IsMatch(source, pattern);
        }
    }

    [TestClass]
    public class IEnumerableExtsTest
    {
        [TestMethod]
        [DataRow("1", false)]
        [DataRow("1a", false)]
        [DataRow("1@", false)]
        [DataRow("1ab", false)]
        [DataRow("1aB", true)]
        [DataRow("1a@", true)]
        [DataRow("1A@", true)]
        [DataRow("aB@", true)]
        public void 如何判斷輸入值必需要符合以下四種條件中的至少三種(string value, bool expected)
        {
            string isDigit = @"\d";
            string islowerLetter = @"[a-z]";
            string isUpperLetter = @"[A-Z]";
            string isSpecialChar = @"[!@#$%^&]";

            string[] values = { "1", "1a", "1@", "1ab", "1aB", "1a@", "1A@", "aB@" };
            bool isMatch = new string[] { isDigit, islowerLetter, isUpperLetter, isSpecialChar }
                .Select<string,Func<string, bool>>(pattern => v => v.IsMatch(pattern))
                .TrueGreaterOrEqualThan(value, 3);
            Assert.AreEqual(expected, isMatch);
        }
    }
}
