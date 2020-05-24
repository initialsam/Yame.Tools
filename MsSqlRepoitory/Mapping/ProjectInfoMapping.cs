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
    public class ProjectInfoMapping : EntityTypeConfiguration<ProjectInfo>
    {
        public ProjectInfoMapping()
        {
            ToTable("ProjectInfos");

            HasKey(x => x.ProjectInfoId);
            Property(x => x.ProjectInfoId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Title).IsRequired().HasMaxLength(100);
            Property(x => x.Detail).IsRequired();
            //多對多
            HasMany(e => e.ProjectTags)
                .WithMany(e => e.ProjectInfos)
                .Map(m => m.ToTable("MAP_ProjectInfos_ProjectTags")
                .MapLeftKey(nameof(ProjectInfo.ProjectInfoId))
                .MapRightKey(nameof(ProjectTag.ProjectTagId)));
        }
    }
}
