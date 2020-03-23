using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.HttpStatusCode;
using Yame.FeatureTests.Dto;
using System.Threading.Tasks;

namespace Yame.FeatureTests
{
    [TestClass]
    public class IEnumerableYield
    {
        [TestMethod]
        public void Test_Yield()
        {
            //Ref https://www.facebook.com/91agile/videos/1408194006021880/?v=1408194006021880
            var girls = new List<Girl>()
              {
                  new Girl{Name="A1" },
                  new Girl{Name="A2" },
                  new Girl{Name="A3" },
                  new Girl{Name="A4" },
                  new Girl{Name="A5" }
              };
            var keys = new List<Key>()
              {
                  new Key{Owner="K1",CarType=CarType.BMW },
                  new Key{Owner="K2",CarType=CarType.TESLA },
                  new Key{Owner="K3",CarType=CarType.TOYOTA }
              };

            var acturl = Pairs(girls, keys, (girl, key) => $"{key.Owner}-{girl.Name}-{key.CarType}");
            var expected = new[]
            {
                "K1-A1-BMW",
                "K2-A2-TESLA",
                "K3-A3-TOYOTA"
            };
            acturl.Should().BeEquivalentTo(expected);
        }

        private IEnumerable<string> Pairs(IEnumerable<Girl> girls, IEnumerable<Key> keys, Func<Girl, Key, string> selector)
        {
            var girlEnumerator = girls.GetEnumerator();
            var keyEnumerator = keys.GetEnumerator();
            while(girlEnumerator.MoveNext() && keyEnumerator.MoveNext())
            {
                var girl = girlEnumerator.Current;
                var key = keyEnumerator.Current;
                yield return selector(girl, key);
            }
        }
    }
    internal enum CarType
    {
        BMW,
        TESLA,
        TOYOTA
    }
    internal class Key
    {
        public CarType CarType { get; set; }
        public string Owner { get; set; }
    }

    internal class Girl
    {
        public string Name { get; set; }
    }
}
