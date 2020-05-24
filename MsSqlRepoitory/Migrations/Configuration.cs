namespace MsSqlRepoitory.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MsSqlRepoitory.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MsSqlRepoitory.DataContext context)
        {
            context.Seed(context);

            base.Seed(context);
        }
    }
}
