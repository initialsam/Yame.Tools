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

            var chars = "0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            var result1 = new string(
               Enumerable.Repeat(chars, 6)
                         .Select(s => s[random.Next(s.Length)])
                         .ToArray());
            var result2 = new string(
               Enumerable.Repeat(chars, 6)
                         .Select(s => s[random.Next(s.Length)])
                         .ToArray());
            var a = Enumerable.Repeat(chars, 6);
            foreach (var item in a)
            {
                var z = random.Next(item.Length);
                var b = item[z];
            }
        }
    
    }


}
