using Autofac;
using Autofac.Integration.Mvc;
using MsSqlRepoitory;
using MsSqlRepoitory.Repositories;
using Service.BackendService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVC5Web
{
    public class AutofacConfig
    {
        /// <summary>
        /// 註冊DI注入物件資料
        /// </summary>
        public static void Register()
        {
            // 容器建立者
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //Register DataRepository
            builder.RegisterType<DataContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            //Register Service
            builder.RegisterType<LocationTagService>().As<ILocationTagService>().InstancePerRequest();

            // OPTIONAL: Register model binders that require DI.
            //builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            //builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();


            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}