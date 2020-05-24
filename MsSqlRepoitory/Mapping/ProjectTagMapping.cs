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
    public class ProjectTagMapping : EntityTypeConfiguration<ProjectTag>
    {
        public ProjectTagMapping()
        {
            ToTable("ProjectTags");

            HasKey(x => x.ProjectTagId);
            Property(x => x.ProjectTagId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProjectTagName).IsRequired().HasMaxLength(100);
        }
    }
}
