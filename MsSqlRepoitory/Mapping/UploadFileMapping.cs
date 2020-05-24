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
    public class UploadFileMapping : EntityTypeConfiguration<UploadFile>
    {
        public UploadFileMapping()
        {
            ToTable("UploadFiles");

            HasKey(x => x.FileId);
            Property(x => x.FileId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OriginalName).IsRequired().HasMaxLength(200);
            Property(x => x.FileName).IsRequired().HasMaxLength(200);
            Property(x => x.FileExtension).IsRequired().HasMaxLength(100);
            Property(x => x.FilePath).IsRequired().HasMaxLength(500);
            //設定關聯 連動刪除
            HasRequired(x => x.ProjectInfo)
                .WithMany(s => s.Files)
                .HasForeignKey(x => x.ProjectInfoId)
                .WillCascadeOnDelete(true);

        }
    }
}
