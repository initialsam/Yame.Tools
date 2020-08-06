using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpectedObjects;
using System.Collections.Generic;

namespace ExpectedObjectsNote
{
    [TestClass]
    public class A01_Collections
    {
        internal class TypeWithIEnumerable
        {
            public List<string> Objects { get; set; }
        }
        [TestMethod]
        public void ComparingEqualCollections_ShouldBeEqual()
        {
            var expected = new TypeWithIEnumerable
            {
                Objects = new List<string> { "test2", "test1" }
            }.ToExpectedObject();

            var actual = new TypeWithIEnumerable
            {
                Objects = new List<string> { "test1", "test2" }
            };

            expected.ShouldEqual(actual);

        }

        [TestMethod]
        public void ComparingEqualCollections_ShouldBeEqualAnd順序要對()
        {
            var expected = new TypeWithIEnumerable
            {
                Objects = new List<string> { "test1", "test2" }
            }.ToExpectedObject(ctx => ctx.UseOrdinalComparison());

            var actual = new TypeWithIEnumerable
            {
                Objects = new List<string> { "test1", "test2" }
            };

            expected.ShouldEqual(actual);
            /*
 1) TypeWithIEnumerable.Objects[0]:

  Expected:
    "test2"
    
  Actual:
    "test1"
    


2) TypeWithIEnumerable.Objects[1]:

  Expected:
    "test1"
    
  Actual:
    "test2"
*/
        }
    }

    
}
