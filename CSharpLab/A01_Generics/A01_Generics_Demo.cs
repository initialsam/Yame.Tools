using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A01_Generics
{
    public class A01_Generics_Demo
    {
        public static IAPIData<Product,int> Demo(string json)
        {
            return JsonHelper.JsonToObject<APIData<Product, int>>(json);
        }
    }
}
