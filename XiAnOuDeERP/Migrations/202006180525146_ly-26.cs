namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly26 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Project", new[] { "ProjectStateId" });
            AlterColumn("dbo.Project", "ProjectStateId", c => c.Long(nullable: false));
            CreateIndex("dbo.Project", "ProjectStateId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Project", new[] { "ProjectStateId" });
            AlterColumn("dbo.Project", "ProjectStateId", c => c.Long());
            CreateIndex("dbo.Project", "ProjectStateId");
        }
    }
}
