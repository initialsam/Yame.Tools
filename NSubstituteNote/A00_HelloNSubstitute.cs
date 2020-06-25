using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace NSubstituteNote
{
    [TestClass]
    public class A00_HelloNSubstitute
    {
        public interface ICalculator
        {      
            int Add(int a, int b);
        }

        [TestMethod]
        public void HelloNSubstitute()
        {
            //arrange
            //NSubstitute會產生一個ICalculator 假的實體出來
            ICalculator calculator = Substitute.For<ICalculator>();
            //設定假的實體的Add方法當傳入3,6 回傳 9
            calculator.Add(3,6).Returns(9);
            var expected = 9;

            //act
            var actual = calculator.Add(3,6);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
