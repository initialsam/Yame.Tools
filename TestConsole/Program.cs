using System;
using System.Collections.Generic;
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
            Console.WriteLine("MachineName: {0}", Environment.MachineName);

            string HostName = Dns.GetHostName();
            Console.WriteLine("Host Name of machine =" + HostName);
            IPAddress[] ipaddress = Dns.GetHostAddresses(HostName);
            Console.Write("IPv4 of Machine is ");
            foreach (IPAddress ip4 in ipaddress.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
            {
                Console.WriteLine(ip4.ToString());
            }
            Console.ReadKey();
        }
    }
}
