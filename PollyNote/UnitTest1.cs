using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PollyNote
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //https://blog.darkthread.net/blog/polly-policy-wrap/
            PolicyWrapDemo.Test();
        }

        [TestMethod]
        public void PolicyRetryTest()
        {
            //https://marcus116.blogspot.com/2019/06/netcore-polly-retry.html
            PolicyRetry.Test();
        }

        [TestMethod]
        public void PolicyTimeoutTest()
        {
            //https://marcus116.blogspot.com/2019/06/netcore-polly-timeout-wrap.html
            //PolicyTimeout.Test();

            PolicyTimeout.TestTimeoutRetry();
        }


        [TestMethod]
        public void PolicyFallbackTest()
        {
            //PolicyFallback.Test();
            PolicyFallback.TestTimeoutRetryFallback();
        }

        [TestMethod]
        public void WrapTest()
        {
            //PolicyFallback.Test();
            new Wrap().Test();
        }
    }
}
