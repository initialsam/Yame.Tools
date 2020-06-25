using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpLab.A04_Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A04_Dictionary.Tests
{
    [TestClass()]
    public class A04_DictionaryTests
    {
        [TestMethod()]
        public void KeyIsCustomClassTest()
        {
            //arrange
            Dictionary<MyClass, string> sut = new Dictionary<MyClass, string>()
            {
                [new MyClass { Id = 1, Name = "a" }] = "aa",
                [new MyClass { Id = 2, Name = "b" }] = "bb",
            };

            var myClassSpecialComparer = new MyClassSpecialComparer();
            var expected = true;

            //act
            var actual = sut.Keys.Contains(
                new MyClass { Id = 1, Name = "a" }, myClassSpecialComparer);
 
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void KeyIsImmutableObjectsTest()
        {
            //arrange
            Dictionary<ImmutableObjects, string> sut = new Dictionary<ImmutableObjects, string>()
            {
                [new ImmutableObjects(1, "a")] = "aa",
                [new ImmutableObjects(2, "b")] = "bb",
            };
            var expected = true;
            //act
            var actual = sut.ContainsKey(new ImmutableObjects(1, "a"));

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}