using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Collections.Generic;
using System.Collections;

namespace FluentAssertionsNote
{
    /// <summary>
    /// 內容都是 官方Wiki範例 https://fluentassertions.com/collections/
    /// </summary>
    [TestClass]
    public class F02_Collections
    {
        public class Order
        {
            public int Id { get; set; }
            public int Price { get; set; }
        }

        [TestMethod]
        public void FluentAssertionsNote11()
        {
            var expected = new List<Order>()
                {
                    new Order { Id=1,Price=10},
                    new Order { Id=2,Price=20},
                    new Order { Id=3,Price=30},

                };

            var actual = new List<Order>()
                {
                    new Order { Id=1,Price=10},
                    new Order { Id=2,Price=20},
                    new Order { Id=3,Price=30},
                };

            //不分先後順序
            //actual.ShouldBeEquivalentTo(expected);
            actual.Should().BeEquivalentTo(expected);
            //順序要一致
            //actual.ShouldBeEquivalentTo(expected, options => options.WithStrictOrdering());


            //若expected的 new Order { Id=1,Price=11}
            //錯誤訊息是
            //Expected item[0].Price to be 11, but found 10.
        }

        [TestMethod]
        public void FluentAssertionsDictionaries()
        {
            Dictionary<int, string> dictionary = null;
            dictionary.Should().BeNull();

            dictionary = new Dictionary<int, string>();
            dictionary.Should().NotBeNull();
            dictionary.Should().BeEmpty();
            dictionary.Add(1, "first element");
            dictionary.Should().NotBeEmpty();
            var dictionary1 = new Dictionary<int, string>
            {
                { 1, "One" },
                { 2, "Two" }
            };

            var dictionary2 = new Dictionary<int, string>
            {
                { 1, "One" },
                { 2, "Two" }
            };

            var dictionary3 = new Dictionary<int, string>
            {
                { 3, "Three" },
            };
            dictionary1.Should().Equal(dictionary2);
            dictionary1.Should().NotEqual(dictionary3);
            dictionary1.Should().ContainKey(1);
            dictionary1.Should().ContainKeys(1, 2);
            dictionary1.Should().NotContainKey(9);
            dictionary1.Should().NotContainKeys(9, 10);
            dictionary1.Should().ContainValue("One");
            dictionary1.Should().ContainValues("One", "Two");
            dictionary1.Should().NotContainValue("Nine");
            dictionary1.Should().NotContainValues("Nine", "Ten");

            dictionary2.Should().HaveCount(2);
            dictionary2.Should().NotHaveCount(3);

            dictionary1.Should().HaveSameCount(dictionary2);
            dictionary1.Should().NotHaveSameCount(dictionary3);

            dictionary1.Should().HaveSameCount(dictionary2.Keys);
            dictionary1.Should().NotHaveSameCount(dictionary3.Keys);
        }

     
    }
}
