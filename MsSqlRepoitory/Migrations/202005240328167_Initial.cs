namespace MsSqlRepoitory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocationTags",
                c => new
                    {
                        LocationTagId = c.Int(nullable: false, identity: true),
                        Sequence = c.Int(nullable: false),
                        LocationName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.LocationTagId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Sequence = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        CreateDate = c.DateTime(nullable: false),
                        ServiceStartDate = c.DateTime(),
                        ServiceEndDate = c.DateTime(),
                        ProjectInfoId = c.Int(nullable: false),
                        LocationTagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.LocationTags", t => t.LocationTagId)
                .ForeignKey("dbo.ProjectInfos", t => t.ProjectInfoId, cascadeDelete: true)
                .Index(t => t.ProjectInfoId)
                .Index(t => t.LocationTagId);
            
            CreateTable(
                "dbo.ProjectInfos",
                c => new
                    {
                        ProjectInfoId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Detail = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsWishingPool = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectInfoId);
            
            CreateTable(
                "dbo.UploadFiles",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        OriginalName = c.String(nullable: false, maxLength: 200),
                        FileName = c.String(nullable: false, maxLength: 200),
                        FileExtension = c.String(nullable: false, maxLength: 100),
                        FilePath = c.String(nullable: false, maxLength: 500),
                        IsCover = c.Boolean(nullable: false),
                        ProjectInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.ProjectInfos", t => t.ProjectInfoId, cascadeDelete: true)
                .Index(t => t.ProjectInfoId);
            
            CreateTable(
                "dbo.ProjectTags",
                c => new
                    {
                        ProjectTagId = c.Int(nullable: false, identity: true),
                        ProjectTagName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ProjectTagId);
            
            CreateTable(
                "dbo.SystemSetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UpdateNote = c.String(),
                        IsForceUpdateNote = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MAP_ProjectInfos_ProjectTags",
                c => new
                    {
                        ProjectInfoId = c.Int(nullable: false),
                        ProjectTagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectInfoId, t.ProjectTagId })
                .ForeignKey("dbo.ProjectInfos", t => t.ProjectInfoId, cascadeDelete: true)
                .ForeignKey("dbo.ProjectTags", t => t.ProjectTagId, cascadeDelete: true)
                .Index(t => t.ProjectInfoId)
                .Index(t => t.ProjectTagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MAP_ProjectInfos_ProjectTags", "ProjectTagId", "dbo.ProjectTags");
            DropForeignKey("dbo.MAP_ProjectInfos_ProjectTags", "ProjectInfoId", "dbo.ProjectInfos");
            DropForeignKey("dbo.Projects", "ProjectInfoId", "dbo.ProjectInfos");
            DropForeignKey("dbo.UploadFiles", "ProjectInfoId", "dbo.ProjectInfos");
            DropForeignKey("dbo.Projects", "LocationTagId", "dbo.LocationTags");
            DropIndex("dbo.MAP_ProjectInfos_ProjectTags", new[] { "ProjectTagId" });
            DropIndex("dbo.MAP_ProjectInfos_ProjectTags", new[] { "ProjectInfoId" });
            DropIndex("dbo.UploadFiles", new[] { "ProjectInfoId" });
            DropIndex("dbo.Projects", new[] { "LocationTagId" });
            DropIndex("dbo.Projects", new[] { "ProjectInfoId" });
            DropTable("dbo.MAP_ProjectInfos_ProjectTags");
            DropTable("dbo.SystemSetting");
            DropTable("dbo.ProjectTags");
            DropTable("dbo.UploadFiles");
            DropTable("dbo.ProjectInfos");
            DropTable("dbo.Projects");
            DropTable("dbo.LocationTags");
        }
    }
}
