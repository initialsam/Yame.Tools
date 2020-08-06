using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A01_Generics
{
    public class APIData<T,U> : IAPIData<T,U>
        where T:class
        where U:struct
    {
        public string Version { get; set; }

        public T Data { get; set; }

        public U ErrorCode { get; set; }
    }
}
