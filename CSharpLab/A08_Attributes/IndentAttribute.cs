using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A08_Attributes
{
    //AllowMultiple 可以掛多個Attribute
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    class IndentAttribute : Attribute 
    {
        public int Count { get; }
        public IndentAttribute(int count = 4)
        {

            Count = count;
        }
    }

}
