using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacNote.Infrastructure
{
    internal class DataRepository : IDataRepository
    {
        public void GetId()
        {
            Console.WriteLine($"DataRepository GetId = 1");
        }
    }
}
