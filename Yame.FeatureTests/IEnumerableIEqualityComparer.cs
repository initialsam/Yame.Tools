using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.HttpStatusCode;
using Yame.FeatureTests.Dto;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Yame.FeatureTests
{
    [TestClass]
    public class IEnumerableIEqualityComparer
    {
        [TestMethod]
        public void Test_IEnumerableYield()
        {
            //Ref https://www.facebook.com/91agile/videos/1408194006021880
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

            IEnumerable<string> Pairs(IEnumerable<Girl> girls, IEnumerable<Key> keys, Func<Girl, Key, string> selector)
            {
                var girlEnumerator = girls.GetEnumerator();
                var keyEnumerator = keys.GetEnumerator();
                while (girlEnumerator.MoveNext() && keyEnumerator.MoveNext())
                {
                    var girl = girlEnumerator.Current;
                    var key = keyEnumerator.Current;
                    yield return selector(girl, key);
                }
            }
        }

        [TestMethod]
        public void Test_SequenceEqual()
        {
            //Ref https://www.facebook.com/91agile/videos/463558427757770/

            var keys1 = new List<Key>()
              {
                  new Key{Owner="K1",CarType=CarType.BMW },
                  new Key{Owner="K2",CarType=CarType.TESLA },
                  new Key{Owner="K3",CarType=CarType.TOYOTA }
              };

            var keys2 = new List<Key>()
              {
                  new Key{Owner="K1",CarType=CarType.BMW },
                  new Key{Owner="K2",CarType=CarType.TESLA },
                  new Key{Owner="K3",CarType=CarType.TOYOTA }
              };

            var acturl = SequenceEqual<Key>(keys1, keys2, new KeyEqualityComparer());

            acturl.Should().BeTrue();

        }

        [TestMethod]
        public void Test_SequenceEqualValueType()
        {
            //Ref https://www.facebook.com/91agile/videos/463558427757770/
            var keys1 = new List<int>() { 1, 4, 7 };
            var keys2 = new List<int>() { 1, 4, 7 };
            var acturl = SequenceEqual<int>(keys1, keys2);
            acturl.Should().BeTrue();
        }

        bool SequenceEqual<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            return SequenceEqual<T>(first, second, EqualityComparer<T>.Default);
        }

        bool SequenceEqual<T>(IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> equalityComparer)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            while (true)
            {
                var firstFlag = firstEnumerator.MoveNext();
                var secondFlag = secondEnumerator.MoveNext();
                if (IsCoutDiff(firstFlag, secondFlag))
                {
                    return false;
                }

                if (IsEnd(firstFlag))
                {
                    return true;
                }

                if (!equalityComparer.Equals(firstEnumerator.Current, secondEnumerator.Current))
                {
                    return false;
                }

            }
        }

        private static bool IsCoutDiff(bool firstFlag, bool secondFlag)
        {
            return firstFlag != secondFlag;
        }

        private static bool IsEnd(bool firstFlag)
        {
            return !firstFlag;
        }
    }

    internal class KeyEqualityComparer : IEqualityComparer<Key>
    {
        public bool Equals(Key x, Key y)
        {
            return x.Owner == y.Owner && x.CarType == y.CarType;
        }

        public int GetHashCode(Key obj)
        {
            return (obj.Owner, obj.CarType).GetHashCode();
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
