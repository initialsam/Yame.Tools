using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace FluentAssertionsNote
{
    /// <summary>
    /// 內容都是 官方範例 https://fluentassertions.com/strings/
    /// </summary>
    [TestClass]
    public class F01_String
    {
        [TestMethod]
        public void FluentAssertionsNote04()
        {
            //string

            string theString = "";
            theString.Should().NotBeNull();
            theString.Should().BeEmpty();
            theString.Should().HaveLength(0);
            theString.Should().BeNullOrWhiteSpace();

            //theString.Should().NotBeEmpty(because:"若失敗出現在失敗訊息中");
            //錯誤訊息Did not expect empty string because 若失敗出現在失敗訊息中.

            //theString.Should().BeNull();
            //錯誤訊息 Expected string to be <null>, but found "".

            theString = "test";
            theString.Should().NotBeEmpty();
            theString.Should().HaveLength(4);
            theString.Should().Be("test"); //有分大小寫
            theString.Should().NotBeNullOrWhiteSpace();
            theString.Should().BeEquivalentTo("TEST"); //不分大小寫

            //theString.Should().Be("TEST"); 
            //錯誤訊息 Expected string to be "TEST", but "test" differs near "tes" (index 0).

            theString = "This is a String";
            theString.Should().StartWith("This");
            theString.Should().StartWithEquivalent("THIS");

            //theString.Should().StartWith("this");
            //錯誤訊息 Expected string to start with"this", but "This is a String" differs near "Thi"(index 0).

            theString.Should().NotStartWith("is");
            theString.Should().NotStartWithEquivalentOf("is");

            theString.Should().Contain("is a");
            theString.Should().ContainEquivalentOf("IS A");
           
            theString.Should().NotContain("are");
            theString.Should().NotContainEquivalentOf("are");

            theString.Should().Match("Th** is a Stri*g");
            theString.Should().MatchEquivalentOf("Th** is a STRI*g");

            theString = "cook";
            theString.Should().MatchRegex("co{0,2}k");

            //theString = "cooook";
            //theString.Should().MatchRegex("co{0,2}k");
            //錯誤訊息 Expected string to match regex"co{0,2}k", but"cooook" does not match.

            theString = "123";
            theString.Should().NotMatchRegex("[a-z]");
            
            theString = null;
            theString.Should().BeNullOrWhiteSpace();
            theString.Should().BeNull();

        }


    }
}
