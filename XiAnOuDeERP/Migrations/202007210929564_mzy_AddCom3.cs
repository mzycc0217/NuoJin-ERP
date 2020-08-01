namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_AddCom3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FnishedProduct_UserDetils", "FnishedProductNumbers", c => c.Double(nullable: false));
            AddColumn("dbo.FnishedProductRoom", "del_or", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FnishedProductRoom", "del_or");
            DropColumn("dbo.FnishedProduct_UserDetils", "FnishedProductNumbers");
        }
    }
}
