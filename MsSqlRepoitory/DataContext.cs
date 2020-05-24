using MsSqlRepoitory.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlRepoitory
{
    public class DataContext: DbContext
    {
        public DataContext()
          : base("name=DefaultConnection")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
            this.Configuration.LazyLoadingEnabled = true;
            //SetLog();
        }

        public virtual IDbSet<Project> Projects { get; set; }
        public virtual IDbSet<ProjectInfo> ProjectInfos { get; set; }
        public virtual IDbSet<ProjectTag> ProjectTags { get; set; }
        public virtual IDbSet<LocationTag> LocationTags { get; set; }
        public virtual IDbSet<SystemSetting> SystemSetting { get; set; }
        public virtual IDbSet<UploadFile> UploadFiles { get; set; }
        public static DataContext Create()
        {
            return new DataContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            AddEntityTypeConfiguration(modelBuilder);

            base.OnModelCreating(modelBuilder);

        }
        /// <summary>
        /// 用反射 將Mapping加入
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void AddEntityTypeConfiguration(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace)
                    && type.BaseType != null
                    && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
                );

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }

        public void Seed(DataContext context)
        {
            DataInitializer.Initialize(context);
        }

        [Conditional("DEBUG")]
        private void SetLog()
        {
            this.Database.Log = (log) => Debug.WriteLine(log);
        }
    }
}
