namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Atlas",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        BatchNumber = c.String(nullable: false, maxLength: 255),
                        Desc = c.String(maxLength: 255),
                        Url = c.String(maxLength: 255),
                        ProjectId = c.Long(nullable: false),
                        TestingTime = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            AddColumn("dbo.Department", "Desc", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Atlas", "ProjectId", "dbo.Project");
            DropIndex("dbo.Atlas", new[] { "ProjectId" });
            DropColumn("dbo.Department", "Desc");
            DropTable("dbo.Atlas");
        }
    }
}
