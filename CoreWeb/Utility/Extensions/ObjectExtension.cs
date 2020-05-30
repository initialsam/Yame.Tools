using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWeb.Utility.Extensions
{
    public static class ObjectExtension
    {
        public static bool IsNull(this object input)
        {
            return input == null;
        }
    }
}
