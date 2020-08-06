using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using FluentAssertions;

//Ref https://claytonone.wordpress.com/2014/06/10/nsubstitute-capturing-arguments-passed-to-a-function/
namespace NSubstituteNote
{
    [TestClass]
    public class A07_ArgDo
    {
        public interface IService
        {
            void MyFunction(int parameterOne);
        }

        public class Service : IService
        {
            public void MyFunction(int parameterOne)
            {
            }
        }

        public class Foo
        {
            private IService Service { get; set; }

            public Foo(IService service)
            {
                this.Service = service;
            }

            public void DoWork()
            {
                this.Service.MyFunction(20);
            }
        }

        [TestMethod]
        public void NSubstituteNote17()
        {
            //arrange
            var service = Substitute.For<IService>();
            var sut = new Foo(service);
            int expected = 20;
            int actul = 0;
            service.MyFunction(Arg.Do<int>(arg => actul = arg));
            //act
            sut.DoWork();
            //assert
            Assert.AreEqual(expected,actul);
        }
    }
}
