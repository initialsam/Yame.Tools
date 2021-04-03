using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A08_Attributes
{
    internal class ProductDebugDisplay
    {
        private readonly ProductB _contact;

        public ProductDebugDisplay(ProductB contact)
        {
            _contact = contact;
        }

        public string UpperName => _contact.Name.ToUpper();

        public string CountInHex => $"16進位:{_contact.Count:X}";
    }
}
