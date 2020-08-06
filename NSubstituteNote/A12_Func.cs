using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using FluentAssertions;

namespace NSubstituteNote
{
    [TestClass]
    public class A12_Func
    {
        public class MyDemo
        {
            private Action<int, int> _myAction { get; }
            private Func<int, bool> _myFunc { get; }

            public MyDemo(Action<int, int> myAction,
                          Func<int, bool> myFunc)
            {
                this._myAction = myAction;
                this._myFunc = myFunc;
            }
            public void DoA(int a,int b,int c,bool flag)
            {
                if (flag)
                {
                    _myFunc(a);
                    _myAction(b,c);
                }
            }
            public void DoB(Func<string, bool> func, bool flag)
            {
                if (flag)
                {
                    func("test");
                }
            }

            public void DoC(Func<string, bool> func, Action<int> action)
            {
                if (func("test"))
                {
                    action(1);
                }
            }
        }

        [TestMethod]
        public void NSubstituteNote23()
        {
            //arrange
            var myAction = Substitute.For<Action<int, int>>();
            var myFunc = Substitute.For<Func<int, bool>>();

            var sut = new MyDemo(myAction, myFunc);
            //act
            sut.DoA(1,2,3,true);
            //assert

            myFunc.ReceivedWithAnyArgs().Invoke(Arg.Any<int>());
            myAction.ReceivedWithAnyArgs().Invoke(Arg.Any<int>(), Arg.Any<int>());

        }

        [TestMethod]
        public void NSubstituteNote24()
        {
            //arrange
            var myAction = Substitute.For<Action<int, int>>();
            var myFunc = Substitute.For<Func<int, bool>>();

            var sut = new MyDemo(myAction, myFunc);
            //act
            sut.DoA(1, 2, 3, false);
            //assert

            myFunc.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
            myAction.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>(), Arg.Any<int>());

        }

        [TestMethod]
        public void NSubstituteNote25()
        {
            //arrange
            var func = Substitute.For<Func<string, bool>>();
            var sut = new MyDemo(null, null);
            //act
            sut.DoB(func, false);
            //assert

            func.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<string>());

        }

        [TestMethod]
        public void NSubstituteNote26()
        {
            //arrange
            var func = Substitute.For<Func<string, bool>>();
            var sut = new MyDemo(null, null);
            //act
            sut.DoB(func, true);
            //assert
            func.ReceivedWithAnyArgs().Invoke(Arg.Any<string>());
        }

        [TestMethod]
        public void NSubstituteNote27()
        {
            //arrange
            var func = Substitute.For<Func<string, bool>>();
            func.Invoke(Arg.Any<string>()).Returns(true);
            var action = Substitute.For<Action<int>>();
            var sut = new MyDemo(null, null);
            //act
            sut.DoC(func, action);
            //assert
            action.ReceivedWithAnyArgs().Invoke(Arg.Any<int>());
        }
        [TestMethod]
        public void NSubstituteNote28()
        {
            //arrange
            var func = Substitute.For<Func<string, bool>>();
            func.Invoke(Arg.Any<string>()).Returns(false);
            var action = Substitute.For<Action<int>>();
            var sut = new MyDemo(null, null);
            //act
            sut.DoC(func, action);
            //assert
            action.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
        }
    }
}
