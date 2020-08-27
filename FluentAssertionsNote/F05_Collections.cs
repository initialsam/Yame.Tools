using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Collections.Generic;

namespace FluentAssertionsNote
{
    /// <summary>
    /// 內容都是 官方Wiki範例 https://github.com/dennisdoomen/fluentassertions/wiki
    /// </summary>
    [TestClass]
    public class F05_Collections
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
            actual.ShouldBeEquivalentTo(expected);
            //順序要一致
            //actual.ShouldBeEquivalentTo(expected, options => options.WithStrictOrdering());

           
            //若expected的 new Order { Id=1,Price=11}
            //錯誤訊息是
            //Expected item[0].Price to be 11, but found 10.
        }

        [TestMethod]
        public void FluentAssertionsNote12()
        {
            //Nullable 

            long? theLong = null;
            theLong.Should().NotHaveValue();

            int? theInt = 3;
            theInt.Should().HaveValue();

            //long? a = null;
            //a.Should().HaveValue();
            //錯誤訊息 Expected a value.

            //int? b = 3;
            //b.Should().NotHaveValue();
            //錯誤訊息 Did not expect a value, but found 3.
        }

        [TestMethod]
        public void FluentAssertionsNote13()
        {
            //Booleans

            bool theBoolean = false;
            theBoolean.Should().BeFalse();
            theBoolean.Should().Be(false);

            theBoolean = true;
            theBoolean.Should().BeTrue();
            theBoolean.Should().Be(true);

            //theBoolean.Should().Be(false);
            //錯誤訊息 boolean to be False, but found True.

        }
    }
}
