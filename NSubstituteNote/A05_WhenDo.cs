using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace NSubstituteNote
{
    [TestClass]
    public class A05_WhenDo
    {
        public class Calculator
        {
            public virtual void Add(int a, int b, out int c, ref bool d)
            {
                c = a + b;
                d = c > 0;
            }
        }

        [TestMethod]
        public void NSubstituteNote12()
        {
            //arrange
            var calculator = Substitute.For<Calculator>();
            var c = 0;
            var d = false;
            calculator.When(x => x.Add(3,6, out c,ref d))
                      .Do(x => 
                        {
                            x[2] = 9;
                            x[3] = true;
                        });

            var expectedOut = 9;
            var expectedRef = true;

            //act
            calculator.Add(3, 6, out c, ref d);

            //assert
            Assert.AreEqual(expectedOut, c);
            Assert.AreEqual(expectedRef, d);
        }

        [TestMethod]
        public void NSubstituteNote13()
        {
            //arrange
            var calculator = Substitute.For<Calculator>();
            var c = 0;
            var d = false;
            var count = 0;
            calculator.When(x => x.Add(3, 6, out c, ref d))
                      .Do(x => count++);

            var expected = 3;

            //act
            calculator.Add(3, 6, out c, ref d);
            calculator.Add(3, 6, out c, ref d);
            calculator.Add(3, 6, out c, ref d);

            //assert
            Assert.AreEqual(expected, count);
        }
    }
}
