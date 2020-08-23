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

            var a1 = Uri.IsWellFormedUriString("https://www.google.com", UriKind.Absolute);
            var a2 = Uri.IsWellFormedUriString("http://www.google.com", UriKind.Absolute);
            var a3 = Uri.IsWellFormedUriString("www.google.com", UriKind.Absolute);
            var a4 = Uri.IsWellFormedUriString("aaa", UriKind.Absolute);
            var a5 = Uri.IsWellFormedUriString("https://aaa", UriKind.Absolute);
            var a6 = Uri.IsWellFormedUriString("http://aaa", UriKind.Absolute);
            var a7 = Uri.IsWellFormedUriString("https://aaa.bbb", UriKind.Absolute);
            var a8 = Uri.IsWellFormedUriString("http://aaa.bbb", UriKind.Absolute);

        }
    
    }


}
