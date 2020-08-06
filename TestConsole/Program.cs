using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int price = 25002;
            var a = (int)System.Math.Round(price * 0.97, 0, MidpointRounding.AwayFromZero);
            var b = (int)System.Math.Round(price * 1.03, 0, MidpointRounding.AwayFromZero);
        }

    }
}
