using NSubstituteNote.EntityFramework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NSubstituteNote.EntityFramework
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(DataContext context) : base(context)
        {
        }



        public Author GetAuthor(int id)
        {
            return PlutoContext.Authors.SingleOrDefault(a => a.Id == id);
        }

        public DataContext PlutoContext
        {
            get { return Context as DataContext; }
        }
    }
}