using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace NSubstituteNote
{
    [TestClass]
    public class A04_RefOut
    {
        public class Calculator
        {
            public virtual string Add(int a, int b, out int c, ref bool d)
            {
                c = a + b;
                d = c > 0;
                return c.ToString();
            }
        }

        [TestMethod]
        public void NSubstituteNote11()
        {
            //arrange
            var calculator = Substitute.For<Calculator>();
            var c = 0;
            var d = false;
            calculator.Add(3, 6, out c, ref d).ReturnsForAnyArgs(x =>
            {
                x[2] = 9;//注意第一個參數是x[0]，所以是X[2]
                //x[2] = (int)x[0]+ (int)x[1]
                x[3] = true;

                return "9";
            });
            var expected = "9";
            var expectedOut = 9;
            var expectedRef = true;

            //act
            var actual = calculator.Add(3, 6, out c, ref d);

            //assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedOut, c);
            Assert.AreEqual(expectedRef, d);
        }
    }
}
