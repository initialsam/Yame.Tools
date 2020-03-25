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
    public class StringBuild
    {
        [TestMethod]
        public void Test_StringBuilder()
        {
            string actual = new StringBuilder()
                   .Append("Hello ")
                   .Append("World ")
                   .ToString()
                   .TrimEnd()
                   .ToUpper();
            string expected = "HELLO WORLD";

            expected.Should().Be(actual);
        }
        [TestMethod]
        public void Test_StringBuilder_Extends()
        {
            //¦Û­qÂX¥R¤èªkAppendWhen
            var isDisabled = false;
            string actual = new StringBuilder()
                                  .Append("<button")
                                  .AppendWhen(" disabled", isDisabled)
                                  .Append(">Click me</button>")
                                  .ToString();
            string expected = "<button>Click me</button>";

            expected.Should().Be(actual);

            isDisabled = true;
            actual = new StringBuilder()
                                  .Append("<button")
                                  .AppendWhen(" disabled", isDisabled)
                                  .Append(">Click me</button>")
                                  .ToString();
            expected = "<button disabled>Click me</button>";

            expected.Should().Be(actual);
        }

      

    }

}
