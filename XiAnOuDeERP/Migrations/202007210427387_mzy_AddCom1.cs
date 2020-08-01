namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_AddCom1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Office_UsrDetils", "OutIutRoom", c => c.Double(nullable: false));
            AddColumn("dbo.Office_UsrDetils", "GetTime", c => c.DateTime());
            AddColumn("dbo.OfficeRoom", "del_or", c => c.Boolean(nullable: false));
            DropColumn("dbo.Office_UsrDetils", "GetRawTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Office_UsrDetils", "GetRawTime", c => c.DateTime());
            DropColumn("dbo.OfficeRoom", "del_or");
            DropColumn("dbo.Office_UsrDetils", "GetTime");
            DropColumn("dbo.Office_UsrDetils", "OutIutRoom");
        }
    }
}
