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
    public class LocationTagMapping : EntityTypeConfiguration<LocationTag>
    {
        public LocationTagMapping()
        {
            ToTable("LocationTags");

            HasKey(x => x.LocationTagId);
            Property(x => x.LocationTagId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.LocationName).IsRequired().HasMaxLength(100);
        }
    }
}
