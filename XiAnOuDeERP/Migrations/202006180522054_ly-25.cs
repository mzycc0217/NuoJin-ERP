namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly25 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectState",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Project", "ProjectStateId", c => c.Long());
            CreateIndex("dbo.Project", "ProjectStateId");
            AddForeignKey("dbo.Project", "ProjectStateId", "dbo.ProjectState", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project", "ProjectStateId", "dbo.ProjectState");
            DropIndex("dbo.Project", new[] { "ProjectStateId" });
            DropColumn("dbo.Project", "ProjectStateId");
            DropTable("dbo.ProjectState");
        }
    }
}
