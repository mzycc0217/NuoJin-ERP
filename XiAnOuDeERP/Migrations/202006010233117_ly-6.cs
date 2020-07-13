namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDetails", "IsCancellation", c => c.Boolean(nullable: false));
            AddColumn("dbo.Department", "IsCancellation", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Department", "IsCancellation");
            DropColumn("dbo.UserDetails", "IsCancellation");
        }
    }
}
