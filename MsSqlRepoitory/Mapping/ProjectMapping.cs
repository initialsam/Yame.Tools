using MsSqlRepoitory.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlRepoitory.Mapping
{
    public class ProjectMapping : EntityTypeConfiguration<Project>
    {
        public ProjectMapping()
        {
            ToTable("Projects");

            HasKey(x => x.ProjectId);
            Property(x => x.ProjectId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Title).IsRequired().HasMaxLength(100);
            //設定關聯 連動不刪除
            HasRequired(t => t.LocationTag)
                .WithMany(t => t.Projects)
                .HasForeignKey(t => t.LocationTagId)
                .WillCascadeOnDelete(false);
        }
    }
}
