namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly34 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RawMaterial", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Atlas", "ProjectId", "dbo.Project");
            DropIndex("dbo.RawMaterial", new[] { "ProjectId" });
            DropIndex("dbo.Atlas", new[] { "ProjectId" });
            DropColumn("dbo.RawMaterial", "ProjectId");
            DropColumn("dbo.Atlas", "ProjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Atlas", "ProjectId", c => c.Long(nullable: false));
            AddColumn("dbo.RawMaterial", "ProjectId", c => c.Long());
            CreateIndex("dbo.Atlas", "ProjectId");
            CreateIndex("dbo.RawMaterial", "ProjectId");
            AddForeignKey("dbo.Atlas", "ProjectId", "dbo.Project", "Id");
            AddForeignKey("dbo.RawMaterial", "ProjectId", "dbo.Project", "Id");
        }
    }
}
