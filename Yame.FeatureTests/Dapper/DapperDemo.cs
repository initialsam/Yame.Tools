using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.FeatureTests.Dapper
{
    [TestClass]
    public class DapperDemo
    {
        [TestMethod]
        public void Test_DapperDemo()
        {
            var repo = new SqLiteCustomerRepository();

            repo.DapperDemo();
        }

    }
}
