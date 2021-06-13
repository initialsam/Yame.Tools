using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using FluentAssertions.Extensions;
using static FluentAssertionsNote.F03_Class;

namespace FluentAssertionsNote
{
    /// <summary>
    /// 內容都是 官方Wiki範例 https://fluentassertions.com/datetimespans/
    /// </summary>
    [TestClass]
    public class F05_DateTime
    {
        
        //public 
        [TestMethod]
        public void FluentAssertionsDateTime1()
        {
            var now = DateTime.Now;
            var expected = new DateTime(2000, 1, 1);
            var actual = new DateTime(2000, 1, 1);

            actual.Should().Be(expected);
            actual.Should().BeAfter(new DateTime(1999, 1, 1));
            actual.Should().BeBefore(new DateTime(2001, 1, 1));

            //expected = new DateTime(2000, 2, 2);
            //actual.Should().Be(expected);
            //錯誤訊息
            // Expected actual to be <2000-02-02>, but found <2000-01-01>.
        }


    }
}
