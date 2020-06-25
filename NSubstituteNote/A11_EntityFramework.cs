using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using NSubstituteNote.EntityFramework.Domain;
using System.Data.Entity;
using System.Linq;
using NSubstituteNote.EntityFramework;

namespace NSubstituteNote
{
    [TestClass]
    public class A11_EntityFramework
    {
        [TestMethod()]
        public void NSubstituteNote22()
        {
            //arrange
            var data = new List<Author>
              {
                 new Author {Id=1, Name = "BBB" },
                 new Author {Id=2, Name = "ZZZ" },
                 new Author {Id=3, Name = "AAA" },
              }.AsQueryable();

            var mockAuthorDbSet = Substitute.For<IDbSet<Author>>().Initialize(data);
            var mockContext = Substitute.For<DataContext>();
            mockContext.Authors.Returns(mockAuthorDbSet);
            var expected = "BBB";
            var sut = new AuthorRepository(mockContext);
            //act
            var actul = sut.GetAuthor(1);
            //assert
            Assert.AreEqual(expected, actul.Name);
        }
    }
}
