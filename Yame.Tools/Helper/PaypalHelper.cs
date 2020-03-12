using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.Helper
{
    public static class PaypalHelper
    {

        public static string GetPriceString(string currency, string value)
        {
            if (value.EndsWith(".00"))
            {
                value = value.Remove(value.Length - 3);
            }
            return $"{currency} : {value}";
        }
    }
}
