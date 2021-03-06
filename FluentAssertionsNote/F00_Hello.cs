using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace FluentAssertionsNote
{
    /// <summary>
    /// 內容都是 官方範例 https://fluentassertions.com/basicassertions/
    /// </summary>
    [TestClass]
    public class F00_Hello
    {

        [TestMethod]
        public void FluentAssertionsNote01()
        {
            string actual = "man"; 
            //口語化 應該是男人
            actual.Should().Be(expected:"man");
          
            //口語化 應該不是abc
            actual.Should().NotBe(unexpected:"abc");

            actual.Should().NotBeNull();
            actual.Should().BeOfType<string>();

            actual = null;
            actual.Should().BeNull(because: "這邊就是打註解的意思");

            //actual = "test";
            //actual.Should().Be(expected: "man");
            //錯誤訊息
            //Expected actual to be "man" with a length of 3, but "test" has a length of 4, differs near "tes" (index 0).
            
            //actual = "abc";
            //actual.Should().Be(expected: "man");
            //錯誤訊息
            //Expected actual to be "man", but "abc" differs near "abc" (index 0).

            //object actual2 = 1;
            //actual2.Should().BeOfType<string>();
            //錯誤訊息
            //Expected type to be System.String, but found System.Int32.
        }

        [TestMethod]
        public void FluentAssertionsNote02()
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
        public void FluentAssertionsNote03()
        {
            //Booleans

            bool theBoolean = false;
            theBoolean.Should().BeFalse();
            theBoolean.Should().Be(false);

            theBoolean = true;
            theBoolean.Should().BeTrue();
            theBoolean.Should().Be(true);

            //theBoolean.Should().Be(false);
            //錯誤訊息
            //Expected theBoolean to be False, but found True.

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
