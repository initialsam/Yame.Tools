using Microsoft.VisualStudio.TestTools.UnitTesting;

using NLog;

using System;

namespace NLogNote
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            ILogger _logger = LogManager.GetCurrentClassLogger();
            var product = new Product { Id = 9, Name = "Test" };
            _logger.Info(product);
            _logger.Info("product={0}", product);
            _logger.Info("product={@0}", product);
            //info: NLogNote.UnitTest1[0]
            //      NLogNote.Product
            //info: NLogNote.UnitTest1[0]
            //      product = NLogNote.Product
            //info: NLogNote.UnitTest1[0]
            //      product ={ "Id":9, "Name":"Test"}
            Console.WriteLine(1111);

        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
