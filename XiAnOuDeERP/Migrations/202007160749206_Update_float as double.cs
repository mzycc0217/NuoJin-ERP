namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_floatasdouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Raw_UserDetils", "RawNumber", c => c.Double(nullable: false));
            AlterColumn("dbo.RawRoom", "RawNumber", c => c.Double(nullable: false));
            AlterColumn("dbo.RawRoom", "RawOutNumber", c => c.Double(nullable: false));
            AlterColumn("dbo.RawRoom", "Warning_RawNumber", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RawRoom", "Warning_RawNumber", c => c.Single(nullable: false));
            AlterColumn("dbo.RawRoom", "RawOutNumber", c => c.Single(nullable: false));
            AlterColumn("dbo.RawRoom", "RawNumber", c => c.Single(nullable: false));
            AlterColumn("dbo.Raw_UserDetils", "RawNumber", c => c.Single(nullable: false));
        }
    }
}
