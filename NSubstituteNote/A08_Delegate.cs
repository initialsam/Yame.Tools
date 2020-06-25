using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using FluentAssertions;

namespace NSubstituteNote
{
    [TestClass]
    public class A08_Delegate
    {
        public interface IService
        {
            Action<int> GetFunc();
        }

        public class MyDemo
        {
            private readonly IService _service;
            public Action Call { get; private set; }
            public MyDemo(IService service)
            {
                this._service = service;
            }
            public void Start()
            {
                this.Call = bosso;
            }
            private void bosso() { }
           
            public void ExecFunc()
            {
                var func = _service.GetFunc();
                func(1);
                func(2);
            }

        }

        [TestMethod]
        public void NSubstituteNote18()
        {
            //arrange
            var service = Substitute.For<IService>();
            var sut = new MyDemo(service);
            var expected = "bosso";
            //act
            sut.Start();
            //assert
            Assert.AreEqual(expected, sut.Call.Method.Name);
        }

        [TestMethod]
        public void NSubstituteNote19()
        {
            //arrange
            var service = Substitute.For<IService>();
            var sut = new MyDemo(service);
            var funcCount = 0;
            service.GetFunc().Returns((x) => funcCount++);
            var expected = 2;
            //act
            sut.ExecFunc();
            //assert
            Assert.AreEqual(expected, funcCount);
        }
    }
}
