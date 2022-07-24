using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpLab;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using FluentAssertions;
using System.Threading;

namespace CSharpLab.Tests
{
    [TestClass()]
    public class FeatureToggleHelperTests
    {
        [TestMethod()]
        public void GetFeatureToggleTest()
        {
            var featureToggleRepo = Substitute.For<IFeatureToggleRepo>();
            featureToggleRepo.GetAllByDatabase().Returns(new List<FeatureToggle> { new FeatureToggle { Code = "A01" } });

            var sut = new FeatureToggleService(featureToggleRepo);
            Thread thread1 = new Thread(() => sut.GetFeatureToggle("A01"));
            Thread thread2 = new Thread(() => sut.GetFeatureToggle("A01"));
            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();
            //var act = sut.GetFeatureToggle("A01");

            featureToggleRepo.Received(1).GetAllByDatabase();
          
            //act.Code.Should().Be("A01");
        }
    }
}