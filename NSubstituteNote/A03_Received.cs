using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace NSubstituteNote
{
    [TestClass]
    public class A03_Received
    {
        public interface ICalculator
        {
            void Clear(int level);
        }

        public class MyDemo
        {
            private readonly ICalculator _calculator;
            public MyDemo(ICalculator calculator)
            {
                this._calculator = calculator;
            }
            public void Action1(bool clearFlag)
            {
                if (clearFlag == true)
                {
                    this._calculator.Clear(7);
                }
            }
          
        }


        public class MyDemo2
        {
            public ICalculator Calculator { get; set; }
         
            public MyDemo2(){}

            public void Action1(bool clearFlag)
            {
                if (clearFlag == true)
                {
                    this.Calculator.Clear(7);
                }
            }

        }

        public class MyDemo3
        {
            private Dictionary<int, MyDemo2> d { get; set; }
            private int key = 0;
          
            private int myVar;

            public MyDemo2 MyDemo2
            {
                get { return d[key]; }
                set {d[key] = value; }
            }

            public MyDemo3()
            {
                d = new Dictionary<int, MyDemo2>();
            }
            public void Action1()
            {
                MyDemo2.Action1(true);
            }
        }

        [TestMethod]
        public void NSubstituteNote9()
        {
            //arrange
            //NSubstitute會產生一個ICalculator 假的實體出來
            ICalculator calculator = Substitute.For<ICalculator>();

            var myDemo = new MyDemo(calculator);

            //act
            myDemo.Action1(true);
            
            //assert
            calculator.Received(1).Clear(7);
            calculator.DidNotReceive().Clear(1);
            calculator.Received(1).Clear(Arg.Any<int>());
            calculator.ReceivedWithAnyArgs(1).Clear(default(int));
        }

        [TestMethod]
        public void NSubstituteNote9B()
        {
            //arrange
            //NSubstitute會產生一個ICalculator 假的實體出來
            ICalculator calculator = Substitute.For<ICalculator>();

            var myDemo3 = new MyDemo3();
            var myDemo2 = new MyDemo2();
            myDemo2.Calculator = calculator;
            myDemo3.MyDemo2 = myDemo2;
            //act
            myDemo3.Action1();

            //assert
            calculator.Received(1).Clear(7);
            calculator.DidNotReceive().Clear(1);
            calculator.Received(1).Clear(Arg.Any<int>());
            calculator.ReceivedWithAnyArgs(1).Clear(default(int));
        }

        [TestMethod]
        public void NSubstituteNote10()
        {
            //arrange
            //NSubstitute會產生一個ICalculator 假的實體出來
            ICalculator calculator = Substitute.For<ICalculator>();

            var myDemo = new MyDemo(calculator);

            //act
            myDemo.Action1(false);

            //assert
            calculator.DidNotReceive().Clear(0);
            calculator.DidNotReceive().Clear(Arg.Any<int>());
            calculator.Received(0).Clear(Arg.Any<int>());
            calculator.DidNotReceiveWithAnyArgs().Clear(default(int));
        }
    }
}
