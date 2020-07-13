namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserType", "Desc", c => c.String());
            AlterColumn("dbo.UserType", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserType", "Name", c => c.String());
            DropColumn("dbo.UserType", "Desc");
        }
    }
}
