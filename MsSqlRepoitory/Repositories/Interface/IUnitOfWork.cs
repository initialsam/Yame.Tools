using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlRepoitory.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        DataContext DataContext { get; }

        void Save();

        Task SaveAsync();
    }
}
