
using Autofac;
using AutofacNote.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacNote
{
    public class AutofacConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
    
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<TimeService>().As<ITimeService>();
            builder.RegisterType<Singleton>().As<ISingleton>().SingleInstance();
            builder.RegisterType<Singleton2>().AsSelf().SingleInstance();
            //builder.Register(c => LogManager.GetLogger(typeof(object))).As<ILog>();
            builder.RegisterModule<RepositoryModule>();
            var container = builder.Build();
            return container;
        }
    }
}
