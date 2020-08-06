using Autofac;
using AutofacNote.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacNote
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var container = AutofacConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Go();

                var singleton = scope.Resolve<ISingleton>();
                singleton.ShowUniqueKey();

                var singleton2 = scope.Resolve<Singleton2>();
                singleton2.ShowUniqueKey();

                var dataRepository = scope.Resolve<IDataRepository>();
                dataRepository.GetId();

            }

            Console.WriteLine("Done! press any key to exit...");
            Console.Read();
        }

       
    }
}