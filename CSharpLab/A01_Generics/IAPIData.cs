using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A01_Generics
{
    public interface IAPIData<T,U>
        where T:class
        where U:struct
    {
        string Version { get; set; }

        T Data { get; set; }

        U ErrorCode { get; set; }
    }
}
