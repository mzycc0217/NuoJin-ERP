namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Raw_UserDetils", "OutIutRoom", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Raw_UserDetils", "OutIutRoom");
        }
    }
}
