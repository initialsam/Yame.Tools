using Serilog;
using System;
using System.Collections.Generic;

namespace SerilogNote
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            const string customTemplate = "Will be logged {Timestamp:yyyy-MMM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";

            ILogger logger1 = new LoggerConfiguration()
                   .WriteTo.File(path: "TT.txt",
                                 rollingInterval: RollingInterval.Day,
                                 outputTemplate: customTemplate,
                                 fileSizeLimitBytes: 1024 * 10)
                   .CreateLogger();

            Log.Logger = logger1;

            var p = new Product
            {
                Age = 20,
                Name = "XXX"
            };
            var p2 = new Product
            {
                Age = 66,
                Name = "ZZZ"
            };
            Log.Logger.Information("Add user {ProductName} ，Age is {PAge}，A is {Product}，B is {@Product}", p.Age, p.Name, p, p2);
            Log.Logger.Information("Add user {ProductName} ，Age is {PAge}，A is {Product}", p.Age, p.Name, p);
            var list1 = new List<int> { 5, 6, 7 };
            var list2 = new List<string> { "A5", "B6", "C7" };
            var list3 = new List<Product> { p, p2 };

            Log.Logger.Information("list1 is {list1}，list2 is {list2}，list3 is {@list3}", list1, list2, list3);

            var d1 = new Dictionary<int, string> { { 1, "aa" }, { 2, "BB" } };
            var d2 = new Dictionary<string, int> { { "YY", 8 }, { "ZZ", 9 } };


            Log.Logger.Information("Dictionary1 is {Dictionary1}，Dictionary2 is {Dictionary2}", d1, d2);


            Console.ReadLine();

        }
    }

    public class Product
    {
        public int Age { get; set; }
        public string Name { get; set; }
    }
}
