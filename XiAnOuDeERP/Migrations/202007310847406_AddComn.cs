namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chemistry_UserDetils", "InenportNumber", c => c.Double(nullable: false));
            AddColumn("dbo.Office_UsrDetils", "InenportNumber", c => c.Double(nullable: false));
            AddColumn("dbo.Raw_UserDetils", "InenportNumber", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Raw_UserDetils", "InenportNumber");
            DropColumn("dbo.Office_UsrDetils", "InenportNumber");
            DropColumn("dbo.Chemistry_UserDetils", "InenportNumber");
        }
    }
}
