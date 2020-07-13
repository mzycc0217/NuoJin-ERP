namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly24 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.User", new[] { "UserTypeId" });
            AlterColumn("dbo.User", "UserTypeId", c => c.Long(nullable: false));
            CreateIndex("dbo.User", "UserTypeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "UserTypeId" });
            AlterColumn("dbo.User", "UserTypeId", c => c.Long());
            CreateIndex("dbo.User", "UserTypeId");
        }
    }
}
