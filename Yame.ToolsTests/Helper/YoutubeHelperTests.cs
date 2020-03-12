using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Yame.Tools.Helper.Tests
{
    [TestClass()]
    public class YoutubeHelperTests
    {
        [DataTestMethod]
        [DataRow("https://www.youtube.com/watch?v=TWKHITWUAnk", "TWKHITWUAnk")]
        [DataRow("https://youtu.be/cyR6Vw--lEQ", "cyR6Vw--lEQ")]
        [DataRow("https://www.youtube.com/watch?time_continue=1&v=m5b2mEWHf_Q", "m5b2mEWHf_Q")]
        [DataRow("", "")]
        public void GetVideoIdTest(string url, string videoId)
        {
            //Arrange

            //Act
            var actual = YoutubeHelper.GetVideoId(url);
            //Assert
            actual.Should().Be(videoId);
        }
    }
}