namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sale", "Url", c => c.String());
            DropColumn("dbo.Sale", "PreSale");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sale", "PreSale", c => c.Double(nullable: false));
            DropColumn("dbo.Sale", "Url");
        }
    }
}
