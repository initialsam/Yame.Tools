using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace NSubstituteNote
{
    [TestClass]
    public class A02_Property
    {
        public interface ICalculator
        {
            string Mode { get; set; }
        }

        [TestMethod]
        public void NSubstituteNote7()
        {
            //arrange
            //NSubstitute會產生一個ICalculator 假的實體出來
            ICalculator calculator = Substitute.For<ICalculator>();
            //設定假的實體當Mode時 回傳
            calculator.Mode.Returns("A");

            var expected = "A";
            //act
            var actual = calculator.Mode;
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NSubstituteNote8()
        {
            //arrange
            //NSubstitute會產生一個ICalculator 假的實體出來
            ICalculator calculator = Substitute.For<ICalculator>();
            //設定假的實體當Mode時 依序回傳A，S，D
            calculator.Mode.Returns("A", "S", "D");

            var expected1 = "A";
            var expected2 = "S";
            var expected3 = "D";

            //act
            var actual1 = calculator.Mode;
            var actual2 = calculator.Mode;
            var actual3 = calculator.Mode;

            //assert
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
            Assert.AreEqual(expected3, actual3);
        }
    }
}
