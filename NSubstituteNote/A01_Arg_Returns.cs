using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Web.Mvc;

namespace NSubstituteNote
{
    [TestClass]
    public class A01_Arg_Returns
    {
        public interface ICalculator
        {
            int Add(int a, int b);
        }

        public class gg
        {
            public virtual int Add(int a, int b)
            {
                return 0;
            }
        }

        [TestMethod]
        public void NSubstituteNote0()
        {
            //arrange
            //NSubstitute會產生一個ICalculator 假的實體出來
            gg gg = Substitute.For<gg>();
            //設定假的實體當傳入1,100回傳101
            gg.Add(1, 100).Returns(101);
            var expected = 101;

            //act
            var actual = gg.Add(1,100);

            //assert
            Assert.AreEqual(expected, actual);

       
        }

        [TestMethod]
        public void NSubstituteNote1()
        {
            //arrange
            //NSubstitute會產生一個ICalculator 假的實體出來
            ICalculator calculator = Substitute.For<ICalculator>();
            //設定假的實體當傳入1,100回傳101
            calculator.Add(1, 100).Returns(101);
            var expected = 101;

            //act
            var actual = calculator.Add(1, 100);

            //assert
            Assert.AreEqual(expected, actual);

            //act 傳入是5,100 所以回傳不會是101
            actual = calculator.Add(5, 100);
            Assert.AreNotEqual(expected, actual);
            //沒設定的參數 會是int的預設值
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void NSubstituteNote2()
        {
            //arrange
            //NSubstitute會產生一個ICalculator 假的實體出來
            ICalculator calculator = Substitute.For<ICalculator>();
            //設定假的實體不管任何參數都回傳222
            calculator.Add(default(int), default(int))
                      .ReturnsForAnyArgs(222);
            var expected = 222;
 
            //act
            var actual = calculator.Add(1, 2);
            var actual2 = calculator.Add(3, 3);
            var actual3 = calculator.Add(19, 200000);

            //assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual2);
            Assert.AreEqual(expected, actual3);
        }

        [TestMethod]
        public void NSubstituteNote3()
        {
            //arrange
            //NSubstitute會產生一個ICalculator 假的實體出來
            ICalculator calculator = Substitute.For<ICalculator>();
            //設定假的實體不管任何參數都回傳333
            calculator.Add(Arg.Any<int>(), Arg.Any<int>())
                      .Returns(333);
            var expected = 333;

            //act
            var actual = calculator.Add(18, 42);
            var actual2 = calculator.Add(13,23);
            var actual3 = calculator.Add(519, 50000);

            //assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual2);
            Assert.AreEqual(expected, actual3);
        }

        [TestMethod]
        public void NSubstituteNote4()
        {
            //arrange
            //NSubstitute會產生一個ICalculator 假的實體出來
            ICalculator calculator = Substitute.For<ICalculator>();
            //設定假的實體不管任何參數，跟6 都回傳333
            calculator.Add(Arg.Any<int>(),6)
                      .Returns(333);
            var expected = 333;

            //act
            var actual = calculator.Add(5,6);

            //assert
            Assert.AreEqual(expected, actual);

            //不符合條件
            var actual3 = calculator.Add(519, 50000);
            Assert.AreNotEqual(expected, actual3);
        }

        [TestMethod]
        public void NSubstituteNote5()
        {
            //arrange
            //NSubstitute會產生一個ICalculator 假的實體出來
            ICalculator calculator = Substitute.For<ICalculator>();
            //設定假的實體大於3，跟小於20 都回傳333
            calculator.Add(Arg.Is<int>(x => x > 3), Arg.Is<int>(x => x < 20))
                      .Returns(333);
            var expected = 333;

            //act
            var actual = calculator.Add(4,19);

            //assert
            Assert.AreEqual(expected, actual);

            //不符合條件
            var actual3 = calculator.Add(3, 20);
            Assert.AreNotEqual(expected, actual3);
        }

        [TestMethod]
        public void NSubstituteNote6()
        {
            //arrange
            //NSubstitute會產生一個ICalculator 假的實體出來
            ICalculator calculator = Substitute.For<ICalculator>();
            //設定假的實體不管任何參數都回傳222
            calculator.Add(default(int), default(int))
                      .ReturnsForAnyArgs(2,22,222);
            var expected1 = 2;
            var expected2 = 22;
            var expected3 = 222;

            //act
            var actual = calculator.Add(1,2);
            var actual2 = calculator.Add(3,3);
            var actual3 = calculator.Add(19,200000);
            var actual4 = calculator.Add(4,44);
            var actual5 = calculator.Add(5,55);

            //assert
            Assert.AreEqual(expected1, actual);
            Assert.AreEqual(expected2, actual2);
            Assert.AreEqual(expected3, actual3);
            //超過最後一個之後就是會回傳最後一個
            Assert.AreEqual(expected3, actual4);
            Assert.AreEqual(expected3, actual5);

        }
    }
}
