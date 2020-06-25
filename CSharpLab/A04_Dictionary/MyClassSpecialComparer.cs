using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A04_Dictionary
{
    //看單元測試
    public class MyClassSpecialComparer : IEqualityComparer<MyClass>
    {
        public bool Equals(MyClass x, MyClass y)
        {
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(MyClass x)
        {
            return x.Id.GetHashCode() + x.Name.GetHashCode();
        }
    }

    public class MyClass
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
