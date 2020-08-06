using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using FluentAssertions;
using System.Threading.Tasks;

namespace NSubstituteNote
{
    [TestClass]
    public class A10_Async
    {
        public interface IService { Task<int> GetCount(); }

        public class MyDemo
        {
            private readonly IService _service;

            public MyDemo(IService service)
            {
                this._service = service;
            }
            public async Task<int> Do()
            {
                var result = await _service.GetCount();
                return result;

            }
        }

        [TestMethod]
        public void NSubstituteNote21()
        {
            //arrange
            var service = Substitute.For<IService>();
            service.GetCount()
                   .ReturnsForAnyArgs(Task.FromResult(66));
            var sut = new MyDemo(service);

            var expected = 66;
            //act
            var actual = sut.Do().Result;
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
