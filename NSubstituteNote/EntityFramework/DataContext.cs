using NSubstituteNote.EntityFramework.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSubstituteNote.EntityFramework
{

    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual IDbSet<Author> Authors { get; set; }
    }
}
