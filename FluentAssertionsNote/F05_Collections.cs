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

            //actual = "test";
            actual.Should().Equal(expected);
            //���~�T�� Expected object to be "man", but found "test".

            //actual = 1;
            //actual.Should().BeOfType<string>();
            //���~�T�� Expected type to be System.String, but found System.Int32
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
