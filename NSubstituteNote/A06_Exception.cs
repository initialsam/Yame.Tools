using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using FluentAssertions;

namespace NSubstituteNote
{
    [TestClass]
    public class A06_Exception
    {
        public class Calculator
        {
            public virtual int Add(int a, int b)
            {
                return a + b;
            }

            public virtual void AddVoid(int a, int b, out int c)
            {
                c = a + b;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NSubstituteNote14()
        {
            //arrange
            var calculator = Substitute.For<Calculator>();
            calculator.Add(1,2).Returns(x => { throw new Exception(); });

            //act
            //assert
            calculator.Add(1,2);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NSubstituteNote15()
        {
            //arrange
            var calculator = Substitute.For<Calculator>();
            var c = 0;
            calculator
                .When(x => x.AddVoid(1,2, out c))
                .Do(x => { throw new Exception(); });

            //act
            //assert
            calculator.AddVoid(1,2, out c);
        }

        [TestMethod]
        public void NSubstituteNote16()
        {
            //ref http://kevintsengtw.blogspot.tw/2015/09/nsubstitute-exception.html
            //arrange
            var calculator = Substitute.For<Calculator>();
            calculator.Add(1, 2).Returns(x => { throw new Exception("FluentAssertions"); });

            //act
            Action act = () => calculator.Add(1, 2);

            //assert
            act.ShouldThrow<Exception>()
               .WithMessage("FluentAssertions");
        }
    }
}
