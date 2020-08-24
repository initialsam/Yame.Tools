using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Collections.Generic;

namespace FluentAssertionsNote
{
    /// <summary>
    /// ���e���O �x��Wiki�d�� https://github.com/dennisdoomen/fluentassertions/wiki
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

            //�������ᶶ��
            actual.ShouldBeEquivalentTo(expected);
            //���ǭn�@�P
            //actual.ShouldBeEquivalentTo(expected, options => options.WithStrictOrdering());

           
            //�Yexpected�� new Order { Id=1,Price=11}
            //���~�T���O
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
            //���~�T�� Expected a value.

            //int? b = 3;
            //b.Should().NotHaveValue();
            //���~�T�� Did not expect a value, but found 3.
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
            //���~�T�� boolean to be False, but found True.

        }
    }
}
