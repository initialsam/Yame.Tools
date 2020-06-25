using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using FluentAssertions;

namespace NSubstituteNote
{
    [TestClass]
    public class A09_InOrder
    {
        public interface IService { int GetCount(); }

        public interface ITotal { int GetTotal(); }

        public interface ISend { void Send(); }

        public class MyDemo
        {
            private readonly IService _service;
            private readonly ITotal _total;
            private readonly ISend _send;

            public MyDemo(IService service,
                          ITotal total,
                          ISend send)
            {
                this._service = service;
                this._total = total;
                this._send = send;
            }
            public void Do()
            {
                _service.GetCount();
                _total.GetTotal();
                _send.Send();
            }
        }

        [TestMethod]
        public void NSubstituteNote20()
        {
            //arrange
            var service = Substitute.For<IService>();
            var total = Substitute.For<ITotal>();
            var send = Substitute.For<ISend>();
            var sut = new MyDemo(service, total, send);
            //act
            sut.Do();
            //assert
            Received.InOrder(() =>
            {
                service.GetCount();
                total.GetTotal();
                send.Send();
            });

            //若順序不對 會是錯誤訊息是
            /*
            Expected to receive these calls in order:

                IService.GetCount()
                ITotal.GetTotal()
                ISend.Send()

            Actually received matching calls in this order:

                ISend.Send()
                IService.GetCount()
                ITotal.GetTotal()
            */
        }
    }
}
