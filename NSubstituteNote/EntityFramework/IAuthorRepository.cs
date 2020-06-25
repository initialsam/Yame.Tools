using NSubstituteNote.EntityFramework.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NSubstituteNote.EntityFramework
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Author GetAuthor(int id);
    }
}
