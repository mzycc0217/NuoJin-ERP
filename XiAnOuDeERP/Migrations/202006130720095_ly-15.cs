namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserType", "Key", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserType", "Key");
        }
    }
}
