using Autofac;
using System.Collections.Generic;
using System.Reflection;

namespace AutofacNote
{
    internal class RepositoryModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = new List<string>
            {
                "AutofacNote"
            };

            assemblies.ForEach(assembly =>
            {
                var module = Assembly.Load(assembly);

                builder.RegisterAssemblyTypes(module)
                    .Where(i => i.Name.EndsWith("Repository"))
                    .AsImplementedInterfaces();
            });

            base.Load(builder);
        }
    }
}