using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Yame.FeatureTests.Dto;

namespace Yame.FeatureTests
{
    /// <summary>
    /// Ref 
    /// 
    /// 
    /// </summary>
    [TestClass]
    public class CSharp8
    {
        [TestMethod]
        public void Test_CSharp8_Discard()
        {
            var ageText = "9";
            var expected = 9;
            //Original
            int age = 0;
            var isInt = int.TryParse(ageText, out age);
            age.Should().Be(expected);
            //New Out Variables
            var isInt2 = int.TryParse(ageText, out int age2);
            age2.Should().Be(expected);
        }


        [TestMethod]
        public void Test_CSharp7_OutVariables()
        {
            var ageText = "9";
            var expected = 9;
            //Original
            int age = 0;
            var isInt = int.TryParse(ageText, out age);
            age.Should().Be(expected);
            //New Out Variables
            var isInt2 = int.TryParse(ageText, out int age2);
            age2.Should().Be(expected);
        }

      
    }
}
